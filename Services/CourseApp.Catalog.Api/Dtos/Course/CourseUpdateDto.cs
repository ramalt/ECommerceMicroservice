using CourseApp.Catalog.Api.Dtos.Feaure;

namespace CourseApp.Catalog.Api.Dtos.Course;

public record CourseUpdateDto(string Id,
                              string Name,
                              string Description,
                              decimal Price,
                              string UserId,
                              string Picture,
                              FeatureCreateDto Feature,
                              string CategoryId);