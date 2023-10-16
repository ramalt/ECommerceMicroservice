namespace CourseApp.Basket.Api.Dtos;

public record BasketItemDto
{
    public int Quantiy { get; set; }
    public string CourseId { get; init; }
    public string CourseName { get; init; }
    public decimal Price { get; init; }
    
}
