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

    [Display(Name = "Tags (separated by a comma)")]
    public string Tags;

    public List<String> _Tags;  
  }
}