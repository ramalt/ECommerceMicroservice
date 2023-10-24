using CourseApp.WebMVC.Models.Catalog;

namespace CourseApp.WebMVC.Services.Interfaces;

public interface ICatalogService
{
    Task<List<CourseViewModel>> GetAllCourseAsync();
    Task<List<CategoryViewModel>> GetAllCategoriesAsync();
    Task<List<CourseViewModel>> GetAllCourseByIdAsync(string userId);
    Task<CourseViewModel> GetCourseByCourseIdAsync(string courseId);
    Task<bool> DeleteCourseAsync(string courseId);
    Task<bool> UpdateCourseAsync(UpdateCourseInput course);
    Task<bool> CreateCourseAsync(CreateCourseInput course);

}
