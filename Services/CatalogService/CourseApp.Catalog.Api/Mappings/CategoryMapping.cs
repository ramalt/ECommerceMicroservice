using AutoMapper;
using CatalogService.CourseApp.Catalog.Api.Dtos.Category;
using CatalogService.CourseApp.Catalog.Api.Models;

namespace CatalogService.CourseApp.Catalog.Api.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<CategoryDto, CategoryCreateDto>().ReverseMap();
    }
}
