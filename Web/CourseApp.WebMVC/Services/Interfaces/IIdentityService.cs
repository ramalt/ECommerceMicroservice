using CourseApp.Shared.Dtos;
using CourseApp.WebMVC.Models;
using IdentityModel.Client;

namespace CourseApp.WebMVC.Services.Interfaces;

public interface IIdentityService
{
    Task<Response<bool>> SingIn(SigninInput signinInput);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();
}
