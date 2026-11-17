using CMS.Core.Dtos;
using CMS.Infrastructure.Services.Categories;
using CMS.Infrastructure.Services.Tracks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Results = CMS.Core.Constants.Results;
using CMS.Infrastructure.Services.Users;

namespace CMS.Web.Controllers
{
    public class TrackController : BaseController
    {
        private readonly ITrackService _trackService;
        private readonly ICategoryService _categoryService;

        public TrackController(
            ITrackService trackService,
            ICategoryService categoryService,
            IUserService userService) : base(userService)
        {
            _trackService = trackService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetTrackData(Pagination pagination, Query query)
        {
            return Json(await _trackService.GetAll(pagination, query));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = new SelectList(
                await _categoryService.GetCategoryList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrackDto dto)
        {
            if (ModelState.IsValid)
            {
                await _trackService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }

            ViewData["categories"] = new SelectList(
                await _categoryService.GetCategoryList(), "Id", "Name", dto.CategoryId);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var track = await _trackService.Get(id);
            ViewData["categories"] = new SelectList(
                await _categoryService.GetCategoryList(), "Id", "Name", track.CategoryId);
            return View(track);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTrackDto dto)
        {
            if (ModelState.IsValid)
            {
                await _trackService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }

            ViewData["categories"] = new SelectList(
                await _categoryService.GetCategoryList(), "Id", "Name", dto.CategoryId);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _trackService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }
    }
}
