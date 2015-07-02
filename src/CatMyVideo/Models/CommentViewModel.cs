using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace CatMyVideo.Models
{
    public class CommentViewModel
    {
        [Required]
        [Display(Name = "Content")]
        public string Text { get; set; }
    }
}