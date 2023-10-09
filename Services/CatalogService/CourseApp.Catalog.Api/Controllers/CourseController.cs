using CatalogService.CourseApp.Catalog.Api.Dtos.Course;
using CatalogService.CourseApp.Catalog.Api.Services;
using CourseApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.CourseApp.Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : CustomBaseController
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var response = await _courseService.GetAllAsync();

        return CreateActionResult(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(string id)
    {
        var response = await _courseService.GetByIdAsync(id);

        return CreateActionResult(response);
    }
    [HttpGet]
    [Route("/api/[controller]/user/{id}/Courses/")]
    public async Task<IActionResult> GetAllCourseByUserId(string id)
    {
        var response = await _courseService.GetAllByUserIdAsync(id);

        return CreateActionResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse(CourseCreateDto request)
    {
        var response = await _courseService.CreateAsync(request);

        return CreateActionResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> AddCourse(CourseUpdateDto request)
    {
        var response = await _courseService.UpdateAsync(request);

        return CreateActionResult(response);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(string id)
    {
        var response = await _courseService.DeleteAsync(id);

        return CreateActionResult(response);

    }
}
