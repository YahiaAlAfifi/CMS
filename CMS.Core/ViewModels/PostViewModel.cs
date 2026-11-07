using System.Collections.Generic;

namespace CMS.Core.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int TimeInMinute { get; set; }
        public CategoryViewModel Category { get; set; }
        public UserViewModel Author { get; set; }
        public string Status { get; set; }
        public List<PostAttachmentViewModel> Attachments { get; set; }
        public string CreatedAt { get; set; }
    }
}
