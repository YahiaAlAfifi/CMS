using System;
using System.Collections.Generic;
using System.Text;
using CMS.Core.Enums;

namespace CMS.Core.Dtos
{
    public class NotificationDto
    {
       public string Title { get; set; }
       public string Body {  get; set; }
       public NotificationAction Action {  get; set; }
       public string ActionId {  get; set; }
    }
}
