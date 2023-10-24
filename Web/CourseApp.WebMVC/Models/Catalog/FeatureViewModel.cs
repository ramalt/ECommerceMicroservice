using System.ComponentModel.DataAnnotations;

namespace CourseApp.WebMVC.Models.Catalog;

public class FeatureViewModel
{
   [Display(Name = "süre")]
    public int Duration { get; set; }
}
