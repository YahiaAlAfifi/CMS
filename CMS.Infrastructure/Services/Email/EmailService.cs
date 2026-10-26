using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send(string to, string subject, string body)
        {
            var username = _configuration["Email:Username"];
            var password = _configuration["Email:Password"];

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException("Email credentials are not configured.");
            }

            using var message = new MailMessage
            {
                From = new MailAddress(username, _configuration["Email:FromName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            message.To.Add(new MailAddress(to));

            using var emailClient = new SmtpClient
            {
                Host = _configuration["Email:Host"],
                Port = _configuration.GetValue<int>("Email:Port"),
                EnableSsl = _configuration.GetValue<bool>("Email:EnableSsl"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };

            await emailClient.SendMailAsync(message);
        }
    }
}
