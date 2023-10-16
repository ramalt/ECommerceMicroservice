using CourseApp.Order.Application.Dtos;
using CourseApp.Shared.Dtos;
using MediatR;

namespace CourseApp.Order.Application.Menu.Commands;

public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
{
    public string UserId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public AddressDto Address { get; set; }
}
