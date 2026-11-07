using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Dtos
{
    public class CreateTrackDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان المقطع")]
        public string Title { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "المدة بالدقائق")]
        public int TimeInMinute { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "المقطع الصوتي")]
        public IFormFile Track { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم صاحب المقطع")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "التصنيف")]
        public int CategoryId { get; set; }
    }
}
