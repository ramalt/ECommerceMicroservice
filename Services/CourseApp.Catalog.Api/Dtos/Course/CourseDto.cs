using CourseApp.Catalog.Api.Dtos.Category;
using CourseApp.Catalog.Api.Dtos.Feaure;

namespace CourseApp.Catalog.Api.Dtos.Course;

public record CourseDto(string Id,
                        string Name,
                        string Description,
                        decimal Price,
                        string Picture,
                        string UserId,
                        DateTime CreatedDate,
                        FeatureDto Feature,
                        CategoryDto Category);
