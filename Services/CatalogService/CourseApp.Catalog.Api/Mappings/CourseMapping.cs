using CatalogService.CourseApp.Catalog.Api.Dtos.Course;
using CatalogService.CourseApp.Catalog.Api.Models;
using AutoMapper;
using MongoDB.Bson;

namespace CatalogService.CourseApp.Catalog.Api.Mappings;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseCreateDto>().ReverseMap();
        CreateMap<Course, CourseUpdateDto>().ReverseMap();
        CreateMap<CourseDto, CourseCreateDto>().ReverseMap();
        CreateMap<BsonDocument, CourseDto>();
    }
}
