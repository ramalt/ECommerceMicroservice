using AutoMapper.Internal.Mappers;
using CourseApp.Order.Application.Config;
using CourseApp.Order.Application.Dtos;
using CourseApp.Order.Infrastructure;
using CourseApp.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.Order.Application.Menu.Queries;

public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
{
    private readonly OrderDbContext _context;

    public GetOrdersByUserIdQueryHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Include(o => o.OrderItems).Where(o => o.BuyerId == request.UserId).ToListAsync();

        if (orders is null)
            return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);

        var orderDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

        return Response<List<OrderDto>>.Success(orderDto, 200);
    }
}
