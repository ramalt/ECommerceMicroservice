using System.ComponentModel.DataAnnotations;

namespace CourseApp.WebMVC.Models;

public class SigninInput
{
    [Display(Name = "Email Adresi")]
    public string Email { get; set; } = null!;

    [Display(Name = "Parola")]
    public string Password { get; set; } = null!;

    [Display(Name = "Beni Hatırla")]

    public bool RememberMe { get; set; } = true;
}
