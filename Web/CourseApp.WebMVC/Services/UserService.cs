using CourseApp.WebMVC.Models;
using CourseApp.WebMVC.Services.Interfaces;

namespace CourseApp.WebMVC.Services;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserViewModel> GetUserData()
    {
        return await _client.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
    }
}
