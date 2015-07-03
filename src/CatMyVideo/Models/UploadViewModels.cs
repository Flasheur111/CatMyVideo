using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CatMyVideo.Models
{
    public class UploadViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(144, ErrorMessage = "The description must not exceed 144 characters.")]
        public string Description { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        [Required]
        [Display(Name = "File")]
        public HttpPostedFileBase File { get; set; }
    }
}
