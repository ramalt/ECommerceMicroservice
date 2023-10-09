using CourseApp.Order.Domain.Core;

namespace CourseApp.Order.Domain.OrderAggregate;

public class OrderItem : Entity
{
    public string ProductId { get; private set; } = null!;
    public string ProductName { get; private set; } = null!;
    public string? PictureUrl { get; private set; }
    public Decimal Price { get; private set; }

    public OrderItem(string productId, string productName, string? pictureUrl, decimal price)
    {
        Price = price;
        PictureUrl = pictureUrl;
        ProductName = productName;
        ProductId = productId;
    }
    public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
    {
        ProductName = productName;
        PictureUrl = pictureUrl;
        Price = price;

    }
}
