using CatalogService.CourseApp.Catalog.Api.Dtos.Category;
using CatalogService.CourseApp.Catalog.Api.Dtos.Feaure;

namespace CatalogService.CourseApp.Catalog.Api.Dtos.Course;

public record CourseDto(string Id,
                        string Name,
                        string Description,
                        decimal Price,
                        string Picture,
                        string UserId,
                        string CategoryId,
                        DateTime CreatedDate,
                        FeatureDto Feature,
                        CategoryDto Category);
