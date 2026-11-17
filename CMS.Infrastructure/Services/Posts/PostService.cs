using AutoMapper;
using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.Exceptions;
using CMS.Core.ViewModels;
using CMS.Data;
using CMS.Data.Models;
using CMS.Infrastructure.Services.Notifications;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly INotificationService _notificationService;

        public PostService(
            CMSDbContext db,
            IMapper mapper,
            IFileService fileService,
            IEmailService emailService,
            INotificationService notificationService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
            _emailService = emailService;
            _notificationService = notificationService;
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Posts
                .Include(x => x.Attachments)
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Where(x => !x.IsDelete &&
                    (string.IsNullOrWhiteSpace(query.GeneralSearch) ||
                     x.Title.Contains(query.GeneralSearch) ||
                     x.Body.Contains(query.GeneralSearch)))
                .AsQueryable();

            var dataCount = await queryString.CountAsync();
            var data = await queryString
                .Skip(pagination.GetSkipValue())
                .Take(pagination.PerPage)
                .ToListAsync();

            return new ResponseDto
            {
                data = _mapper.Map<List<PostViewModel>>(data),
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pagination.GetPages(dataCount),
                    total = dataCount
                }
            };
        }

        public async Task<UpdatePostDto> Get(int id)
        {
            var post = await _db.Posts
                .Include(x => x.Attachments)
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<UpdatePostDto>(post);
            dto.PostAttachments = _mapper.Map<List<PostAttachmentViewModel>>(post.Attachments);
            return dto;
        }

        public async Task<int> Create(CreatePostDto dto)
        {
            var post = _mapper.Map<Post>(dto);
            post.Status = ContentStatus.Pending;

            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
            await SaveAttachments(post.Id, dto.Attachments);
            return post.Id;
        }

        public async Task<int> Update(UpdatePostDto dto)
        {
            var post = await _db.Posts.SingleOrDefaultAsync(x => x.Id == dto.Id && !x.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }

            _mapper.Map(dto, post);
            _db.Posts.Update(post);
            await _db.SaveChangesAsync();
            await SaveAttachments(post.Id, dto.Attachments);
            return post.Id;
        }

        public async Task<int> Delete(int id)
        {
            var post = await _db.Posts.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }

            post.IsDelete = true;
            _db.Posts.Update(post);
            await _db.SaveChangesAsync();
            return post.Id;
        }

        public async Task<int> RemoveAttachment(int id)
        {
            var attachment = await _db.PostAttachments.SingleOrDefaultAsync(x => x.Id == id);
            if (attachment == null)
            {
                throw new EntityNotFoundException();
            }

            _db.PostAttachments.Remove(attachment);
            await _db.SaveChangesAsync();
            return attachment.Id;
        }

        public async Task<int> UpdateStatus(int id, ContentStatus status)
        {
            var post = await _db.Posts
                .Include(x => x.Author)
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (post == null)
            {
                throw new EntityNotFoundException();
            }

            var changeLog = new ContentChangeLog
            {
                ContentId = post.Id,
                Type = ContentType.Post,
                Old = post.Status,
                New = status,
                ChangeAt = System.DateTime.Now
            };

            await _db.ContentChangeLogs.AddAsync(changeLog);
            post.Status = status;
            _db.Posts.Update(post);
            await _db.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(post.Author?.Email))
            {
                await _emailService.Send(
                    post.Author.Email,
                    "Post status updated",
                    $"Your post '{post.Title}' status has changed to {status}.");
            }

            if (!string.IsNullOrWhiteSpace(post.Author?.FCMToken))
            {
                await _notificationService.SendByFCM(
                    post.Author.FCMToken,
                    new NotificationDto
                    {
                        Title = "Post status updated",
                        Body = $"Your post '{post.Title}' status has changed to {status}.",
                        Action = NotificationAction.General,
                        ActionId = post.Id.ToString()
                    });
            }

            return post.Id;
        }

        public async Task<List<ContentChangeLogViewModel>> GetLog(int id)
        {
            var logs = await _db.ContentChangeLogs
                .Where(x => x.ContentId == id && x.Type == ContentType.Post)
                .OrderByDescending(x => x.ChangeAt)
                .ToListAsync();

            return _mapper.Map<List<ContentChangeLogViewModel>>(logs);
        }

        private async Task SaveAttachments(int postId, IEnumerable<Microsoft.AspNetCore.Http.IFormFile> attachments)
        {
            if (attachments == null)
            {
                return;
            }

            foreach (var attachment in attachments.Where(x => x != null && x.Length > 0))
            {
                await _db.PostAttachments.AddAsync(new PostAttachment
                {
                    PostId = postId,
                    AttachmentUrl = await _fileService.SaveFile(attachment, FolderNames.ImagesFolder)
                });
            }

            await _db.SaveChangesAsync();
        }
    }
}
