using System.ComponentModel.DataAnnotations;

namespace CourseApp.WebMVC.Models;

public class SigninInput
{
    [Required(ErrorMessage = "*Bu alanı boş bırakamazsın")]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "*Bu alanı boş bırakamazsın")]
    [Display(Name = "Parola")]
    public string Password { get; set; } = null!;

    [Display(Name = "Beni Hatırla")]
    public bool RememberMe { get; set; } 
}
