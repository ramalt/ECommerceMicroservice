using CatalogService.CourseApp.Catalog.Api.Dtos.Category;
using CatalogService.CourseApp.Catalog.Api.Services;
using CourseApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.CourseApp.Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : CustomBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById( string id)
    {
        var response = await _categoryService.GetByIdAsync(id);
        return CreateActionResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var response = await _categoryService.GetAllAsync();
        return CreateActionResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoryCreateDto request)
    {
        var response = await _categoryService.CreateAsync(request);
        return CreateActionResult(response);
    }

}
