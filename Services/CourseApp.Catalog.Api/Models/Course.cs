using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CourseApp.Catalog.Api.Models;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    public string Picture { get; set; } = null!;

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedDate { get; set; }

    public string UserId { get; set; } = null!;

    public Feature Feature { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; } = null!;

    [BsonIgnore]
    public Category Category { get; set; } = null!;
}
