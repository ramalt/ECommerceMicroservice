using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.WebMVC.Models.Catalog;

public class CreateCourseInput
{
        [Display(Name = "Kurs ismi")]
        public string Name { get; set; }

        [Display(Name = "Kurs açıklaması")]
        public string Description { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        public string Picture { get; set; }

        public string UserId { get; set; }

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Kategori")]
        public string CategoryId { get; set; }

        [Display(Name = "Kurs Resmi")]
        public IFormFile PhotoFormFile { get; set; }
}
