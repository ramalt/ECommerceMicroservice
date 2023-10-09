using AutoMapper;
using CatalogService.CourseApp.Catalog.Api.Dtos.Feaure;
using CatalogService.CourseApp.Catalog.Api.Models;

namespace CatalogService.CourseApp.Catalog.Api.Mappings;

public class FeatureMapping : Profile
{
    public FeatureMapping()
    {
        CreateMap<Feature, FeatureDto>().ReverseMap();
        CreateMap<Feature, FeatureCreateDto>().ReverseMap();
    }
}
