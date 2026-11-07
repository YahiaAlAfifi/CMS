using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Posts
{
    public interface IPostService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<UpdatePostDto> Get(int id);
        Task<int> Create(CreatePostDto dto);
        Task<int> Update(UpdatePostDto dto);
        Task<int> Delete(int id);
        Task<int> UpdateStatus(int id, ContentStatus status);
        Task<List<ContentChangeLogViewModel>> GetLog(int id);
    }
}
