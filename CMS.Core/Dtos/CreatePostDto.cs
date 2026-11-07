using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Dtos
{
    public class CreatePostDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان الخبر")]
        public string Title { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "محتوى الخبر")]
        public string Body { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "مدة القراءة بالدقائق")]
        public int TimeInMinute { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "التصنيف")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الكاتب")]
        public string AuthorId { get; set; }

        [Display(Name = "المرفقات")]
        public List<IFormFile> Attachments { get; set; }
    }
}
