using System.Threading.Tasks;

namespace CMS.Infrastructure.Services
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string body);
    }
}
