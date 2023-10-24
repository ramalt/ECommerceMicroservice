using CourseApp.WebMVC.Models;

namespace CourseApp.WebMVC.Services.Interfaces;

public interface IUserService
{
    Task<UserViewModel> GetUserData();    
}
