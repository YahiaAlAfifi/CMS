using CMS.Core.Dtos;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Notifications
{
    public interface INotificationService
    {
        Task<bool> SendByFCM(string token, NotificationDto dto);
    }
}