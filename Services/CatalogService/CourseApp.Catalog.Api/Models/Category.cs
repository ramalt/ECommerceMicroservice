using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogService.CourseApp.Catalog.Api.Models;

public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
}
