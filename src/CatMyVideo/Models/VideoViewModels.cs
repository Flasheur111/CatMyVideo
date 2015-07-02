using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatMyVideo.Models
{
    public class EditVideoViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Tags")]
        [DataType(DataType.Text)]
        public string Tags { get; set; }

        public List<String> _Tags;
    }

    public class CommentViewModel
    {
        [Required]
        [Display(Name = "Content")]
        public string Content;
    }
}