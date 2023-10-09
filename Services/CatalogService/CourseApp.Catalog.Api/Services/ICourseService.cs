using CourseApp.Catalog.Api.Dtos.Course;
using CourseApp.Shared.Dtos;

namespace CourseApp.Catalog.Api.Services;

public interface ICourseService
{
    Task<Response<List<CourseDto>>> GetAllAsync();
    Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string id);
    Task<Response<CourseDto>> GetByIdAsync(string id);
    Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
    Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
    Task<Response<NoContent>> DeleteAsync(string id);
}
