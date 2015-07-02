using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatMyVideo.Models
{
    public class EditVideoViewModel
    {
        public int Id;

        [Required]
        [Display(Name = "Title")]
        public string Title;

        [Required]
        [Display(Name = "Description")]
        public string Description;

        [RegularExpression("^([a-zA-Z0-9_-]{0,20})?( [a-zA-Z0-9_-]{0,20})?$", ErrorMessage = "Tags must be 20 charaters long and contains only number, letter and -")]
        [Display(Name = "Tags (separated by a comma)")]
        public string Tags;

        public List<String> _Tags;
    }

    public class CommentViewModel
    {
        [Required]
        [Display(Name = "Content")]
        public string Content;
    }
}