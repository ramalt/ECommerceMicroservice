using CourseApp.WebMVC.Models.PhotoService;

namespace CourseApp.WebMVC.Services.Interfaces;

public interface IPhotoService
{
    Task<PhotoViewModel> UploadImage(IFormFile image);   
    Task<bool> DeleteImage(string url);
}
