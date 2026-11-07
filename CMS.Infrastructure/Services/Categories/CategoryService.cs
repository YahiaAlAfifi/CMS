using AutoMapper;
using CMS.Core.ViewModels;
using CMS.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;

        public CategoryService(CMSDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetCategoryList()
        {
            var categories = await _db.Categories
                .Where(x => !x.IsDelete)
                .ToListAsync();

            return _mapper.Map<List<CategoryViewModel>>(categories);
        }
    }
}
