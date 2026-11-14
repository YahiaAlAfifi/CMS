using Results = CMS.Core.Constants.Results;
using CMS.Core.Dtos;
using CMS.Infrastructure.Services.Advertisements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
    public class AdvertisementController : BaseController
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAdvertisementData(Pagination pagination, Query query)
        {
            var result = await _advertisementService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["owners"] = new SelectList(
                await _advertisementService.GetAdvertisementOwners(),
                "Id",
                "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertisementDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                ModelState.Remove("Owner.FullName");
                ModelState.Remove("Owner.Email");
                ModelState.Remove("Owner.PhoneNumber");
            }

            if (ModelState.IsValid)
            {
                await _advertisementService.Create(dto);
                return Ok(Results.AddSuccessResult());
            }

            ViewData["owners"] = new SelectList(
                await _advertisementService.GetAdvertisementOwners(),
                "Id",
                "FullName",
                dto.OwnerId);

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewData["owners"] = new SelectList(
                await _advertisementService.GetAdvertisementOwners(),
                "Id",
                "FullName");

            return View(await _advertisementService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAdvertisementDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                ModelState.Remove("Owner.FullName");
                ModelState.Remove("Owner.Email");
                ModelState.Remove("Owner.PhoneNumber");
            }

            if (ModelState.IsValid)
            {
                await _advertisementService.Update(dto);
                return Ok(Results.EditSuccessResult());
            }

            ViewData["owners"] = new SelectList(
                await _advertisementService.GetAdvertisementOwners(),
                "Id",
                "FullName",
                dto.OwnerId);

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _advertisementService.Delete(id);
            return Ok(Results.DeleteSuccessResult());
        }
    }
}
