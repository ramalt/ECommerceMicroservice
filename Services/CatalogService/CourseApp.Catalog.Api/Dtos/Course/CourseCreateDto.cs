using CatalogService.CourseApp.Catalog.Api.Dtos.Feaure;

namespace CatalogService.CourseApp.Catalog.Api.Dtos.Course;

public record CourseCreateDto(string Name,
                              string Description,
                              decimal Price,
                              string UserId,
                              string Picture,
                              FeatureCreateDto Feature,
                              string CategoryId);
