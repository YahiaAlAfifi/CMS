using AutoMapper;
using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.Exceptions;
using CMS.Core.ViewModels;
using CMS.Data;
using CMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Tracks
{
    public class TrackService : ITrackService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public TrackService(CMSDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Tracks
                .Include(x => x.Category)
                .Include(x => x.PublishedBy)
                .Where(x => !x.IsDelete &&
                    (string.IsNullOrWhiteSpace(query.GeneralSearch) ||
                     x.Title.Contains(query.GeneralSearch) ||
                     x.OwnerName.Contains(query.GeneralSearch)))
                .AsQueryable();

            var dataCount = await queryString.CountAsync();
            var data = await queryString
                .Skip(pagination.GetSkipValue())
                .Take(pagination.PerPage)
                .ToListAsync();

            return new ResponseDto
            {
                data = _mapper.Map<List<TrackViewModel>>(data),
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pagination.GetPages(dataCount),
                    total = dataCount
                }
            };
        }

        public async Task<UpdateTrackDto> Get(int id)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateTrackDto>(track);
        }

        public async Task<int> Create(CreateTrackDto dto)
        {
            var track = _mapper.Map<Track>(dto);
            track.Status = ContentStatus.Pending;
            track.TrackUrl = await _fileService.SaveFile(dto.Track, FolderNames.TracksFolder);

            await _db.Tracks.AddAsync(track);
            await _db.SaveChangesAsync();
            return track.Id;
        }

        public async Task<int> Update(UpdateTrackDto dto)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(x => x.Id == dto.Id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }

            _mapper.Map(dto, track);
            if (dto.Track != null)
            {
                track.TrackUrl = await _fileService.SaveFile(dto.Track, FolderNames.TracksFolder);
            }

            _db.Tracks.Update(track);
            await _db.SaveChangesAsync();
            return track.Id;
        }

        public async Task<int> Delete(int id)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }

            track.IsDelete = true;
            _db.Tracks.Update(track);
            await _db.SaveChangesAsync();
            return track.Id;
        }
    }
}
