using CourseApp.Shared.Dtos;
using CourseApp.WebMVC.Models.Catalog;
using CourseApp.WebMVC.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CourseApp.WebMVC.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CatalogService> _logger;
    private readonly IPhotoService  _photoService;

    public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger, IPhotoService photoService)
    {
        _httpClient = httpClient;
        _logger = logger;
        _photoService = photoService;
    }

    public async Task<bool> CreateCourseAsync(CreateCourseInput course)
    {
        var resultPhotoService = await _photoService.UploadImage( course.PhotoFormFile);
        if (resultPhotoService is not null)
        {
            course.Picture = resultPhotoService.Url;
        }
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
        var response = await _httpClient.GetAsync("category");
        List<CategoryViewModel> categoryList = new();

        if (!response.IsSuccessStatusCode)
        {
            return categoryList;
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
        var dataToken = responseObject["data"];

        if (dataToken is not null)
        {
            foreach (var item in dataToken)
            {
                CategoryViewModel category = new()
                {
                    Id = item["id"].ToString(),
                    Name = item["name"].ToString()
                };
                categoryList.Add(category);
            }
        }
        return categoryList;
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
