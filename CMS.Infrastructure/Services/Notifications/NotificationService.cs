using CMS.Core.Dtos;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        public async Task<bool> SendByFCM(
            string token, NotificationDto dto)
        {
            try
            {
                var message = new Message
                {
                    Data = new Dictionary<string, string>
                    {
                        { "Title", dto.Title },
                        { "Body", dto.Body },
                        { "Action", dto.Action.ToString() },
                        { "ActionId", dto.ActionId }
                    },

                    Fid = token
                };

                await FirebaseMessaging.DefaultInstance.SendAsync(message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
