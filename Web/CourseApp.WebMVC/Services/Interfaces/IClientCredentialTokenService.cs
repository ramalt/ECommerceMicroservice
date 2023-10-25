namespace CourseApp.WebMVC.Services.Interfaces;

public interface IClientCredentialTokenService
{
    Task<string> GetToken();
}
