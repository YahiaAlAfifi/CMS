using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CMS.Web.Models;
using CMS.Infrastructure.Services.Dashboard;
using System.Threading.Tasks;

namespace CMS.Web.Controllers;

public class HomeController : BaseController
{
    private readonly IDashboardService _dashboardService;

    public HomeController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _dashboardService.GetData());
    }

    public async Task<IActionResult> GetUserTypeChartData()
    {
        return Ok(await _dashboardService.GetUserTypeChart());
    }

    public async Task<IActionResult> GetContentTypeChartData()
    {
        return Ok(await _dashboardService.GetContentTypeChart());
    }

    public async Task<IActionResult> GetContentByMonthChartData()
    {
        return Ok(await _dashboardService.GetContentByMonthChart());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
