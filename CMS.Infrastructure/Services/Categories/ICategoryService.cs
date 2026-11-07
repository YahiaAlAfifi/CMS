using CMS.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategoryList();
    }
}
