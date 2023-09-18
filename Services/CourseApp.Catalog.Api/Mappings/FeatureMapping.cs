using AutoMapper;
using CourseApp.Catalog.Api.Dtos.Feaure;
using CourseApp.Catalog.Api.Models;

namespace CourseApp.Catalog.Api.Mappings;

public class FeatureMapping : Profile
{
    public FeatureMapping()
    {
        CreateMap<Feature, FeatureDto>().ReverseMap();
    }
}
