using CMS.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetData();
        Task<List<PieChartViewModel>> GetUserTypeChart();
        Task<List<PieChartViewModel>> GetContentTypeChart();
        Task<List<PieChartViewModel>> GetContentByMonthChart();
    }
}
