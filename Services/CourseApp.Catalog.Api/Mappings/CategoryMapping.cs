using AutoMapper;
using CourseApp.Catalog.Api.Dtos.Category;
using CourseApp.Catalog.Api.Models;

namespace CourseApp.Catalog.Api.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<CategoryDto, CategoryCreateDto>().ReverseMap();
    }
}
