using CMS.Core.Dtos;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Tracks
{
    public interface ITrackService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<UpdateTrackDto> Get(int id);
        Task<int> Create(CreateTrackDto dto);
        Task<int> Update(UpdateTrackDto dto);
        Task<int> Delete(int id);
    }
}
