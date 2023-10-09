using CourseApp.Order.Domain.Core;

namespace CourseApp.Order.Domain.OrderAggregate;

public class Order : AggregateRoot
{
    public DateTime CreatedDate { get; private set; }
    public Address Address { get; private set; } = null!;
    public string BuyerId { get; private set; }
    private readonly List<OrderItem> _orderItems;
    public IReadOnlyList<OrderItem> OrderItems => _orderItems;

    public Order(string buyerId, Address address)
    {
        _orderItems = new List<OrderItem>();
        CreatedDate = DateTime.Now;
        BuyerId = buyerId;
        Address = address;
    }

    public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
    {
        var existProduct = _orderItems.Exists(o => o.ProductId == productId);
        if (!existProduct)
        {
            OrderItem newOrderItem = new OrderItem(productId, productName, pictureUrl, price);

            _orderItems.Add(newOrderItem);
        }
    }
    public decimal GetTotalPrice => _orderItems.Sum(o => o.Price);

}
