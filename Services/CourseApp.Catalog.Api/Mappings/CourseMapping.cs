using AutoMapper;
using CourseApp.Catalog.Api.Dtos.Course;
using CourseApp.Catalog.Api.Models;

namespace CourseApp.Catalog.Api.Mappings;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseCreateDto>().ReverseMap();
        CreateMap<Course, CourseUpdateDto>().ReverseMap();
    }
}
