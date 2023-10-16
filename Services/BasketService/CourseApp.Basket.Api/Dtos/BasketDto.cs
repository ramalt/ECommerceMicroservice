namespace CourseApp.Basket.Api.Dtos;

public record BasketDto
{
    public string UserId { get; init; }    
    public string DiscountCode { get; init; }    
    public List<BasketItemDto> BasketItems { get; init; }    
    public decimal TotalPrice 
    {
        get => BasketItems.Sum(i => i.Price*i.Quantiy);
    } 
}
