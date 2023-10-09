using CatalogService.CourseApp.Catalog.Api.Dtos.Category;
using CatalogService.CourseApp.Catalog.Api.Models;
using CourseApp.Shared.Dtos;
using MongoDB.Bson;

namespace CatalogService.CourseApp.Catalog.Api.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> GetByIdAsync(string id);
    Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);


}
