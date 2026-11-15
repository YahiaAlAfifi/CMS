using CMS.Core.Dtos;
using CMS.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<string> Create(CreateUserDto dto);
        Task<string> Update(UpdateUserDto dto);
        Task<string> Delete(string Id);
        Task<UpdateUserDto> Get(string Id);
        Task<List<UserViewModel>> GetAuthorList();
        UserViewModel GetUserByUsername(string username);
    }
}
