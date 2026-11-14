using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Infrastructure.Services.Categories;
using CMS.Infrastructure.Services.Posts;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Results = CMS.Core.Constants.Results;

namespace CMS.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public PostController(
            IPostService postService,
            ICategoryService categoryService,
            IUserService userService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetPostData(Pagination pagination, Query query)
        {
            return Json(await _postService.GetAll(pagination, query));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLists();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto dto)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }

            await LoadLists(dto.CategoryId, dto.AuthorId);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var post = await _postService.Get(id);
            await LoadLists(post.CategoryId, post.AuthorId);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDto dto)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }

            await LoadLists(dto.CategoryId, dto.AuthorId);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id, ContentStatus status)
        {
            await _postService.UpdateStatus(id, status);
            return Ok(Results.EditSuccessResult());
        }

        [HttpGet]
        public async Task<IActionResult> GetLog(int id)
        {
            return View(await _postService.GetLog(id));
        }

        private async Task LoadLists(int? categoryId = null, string authorId = null)
        {
            ViewData["categories"] = new SelectList(
                await _categoryService.GetCategoryList(), "Id", "Name", categoryId);
            ViewData["authors"] = new SelectList(
                await _userService.GetAuthorList(), "Id", "FullName", authorId);
        }
    }
}
