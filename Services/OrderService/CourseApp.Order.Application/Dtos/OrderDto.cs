namespace CourseApp.Order.Application.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public AddressDto Address { get; set; } = null!;
    public string BuyerId { get; set; } = null!;
    public List<OrderItemDto> OrderItems { get; set; } = null!;
}
