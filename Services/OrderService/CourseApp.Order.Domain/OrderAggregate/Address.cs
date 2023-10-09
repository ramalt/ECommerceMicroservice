namespace CourseApp.Order.Domain.OrderAggregate;

public record Address
{
    public string Province { get; init; } = null!;
    public string District { get; init; } = null!;
    public string Street { get; init; } = null!;
    public string ZipCode { get; init; } = null!;
    public string Line { get; init; } = null!;
}
