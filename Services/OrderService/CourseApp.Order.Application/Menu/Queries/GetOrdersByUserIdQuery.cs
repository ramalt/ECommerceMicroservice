using CourseApp.Order.Application.Dtos;
using CourseApp.Shared.Dtos;
using MediatR;

namespace CourseApp.Order.Application.Menu.Queries;

public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
{
    public string UserId { get; set; }
}
