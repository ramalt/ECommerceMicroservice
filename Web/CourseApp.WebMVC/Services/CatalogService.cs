using System.Net.Http.Json;
using CourseApp.Shared.Dtos;
using CourseApp.WebMVC.Models.Catalog;
using CourseApp.WebMVC.Services.Interfaces;
using CourseApp.WebMVC.Settings;

namespace CourseApp.WebMVC.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateCourseAsync(CreateCourseInput course)
    {
        var response = await _httpClient.PostAsJsonAsync<CreateCourseInput>("course", course);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = await _httpClient.DeleteAsync($"course/{courseId}");

        return response.IsSuccessStatusCode;
    }

    public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("/category");

        //TODO: response unsuccessful, Because, Catalog Service returns Unauthorize exception :/
        if (!response.IsSuccessStatusCode)
            return new List<CategoryViewModel>
            {
                new CategoryViewModel { Id = "0", Name = "Asp.Net Core MVC" },
                new CategoryViewModel { Id = "1", Name = "Asp.Net Core API" },
                new CategoryViewModel { Id = "2", Name = ".NET Microservices" },
            };

        var responseSuccess = await response.Content.ReadFromJsonAsync<
            Response<List<CategoryViewModel>>
        >();
        return responseSuccess.Data;
    }

    public async Task<List<CourseViewModel>> GetAllCourseAsync()
    {
        //http://localhost:5000/services/catalog/course
        var response = await _httpClient.GetAsync("/course");
        if (!response.IsSuccessStatusCode)
            return null;

        var responseSuccess = await response.Content.ReadFromJsonAsync<
            Response<List<CourseViewModel>>
        >();
        return responseSuccess.Data;
    }

    public async Task<List<CourseViewModel>> GetAllCourseByIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"/course/user/{userId}/courses");

        if (!response.IsSuccessStatusCode)
            return null;

        var responseSuccess = await response.Content.ReadFromJsonAsync<
            Response<List<CourseViewModel>>
        >();
        return responseSuccess.Data;
    }

    public async Task<CourseViewModel> GetCourseByCourseIdAsync(string courseId)
    {
        var response = await _httpClient.GetAsync($"/course/{courseId}");

        if (!response.IsSuccessStatusCode)
            return null;

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
        return responseSuccess.Data;
    }

    public async Task<bool> UpdateCourseAsync(UpdateCourseInput course)
    {
        var response = await _httpClient.PutAsJsonAsync<UpdateCourseInput>("course", course);

        return response.IsSuccessStatusCode;
    }
}
