using CourseApp.Order.Application.Dtos;
using CourseApp.Order.Domain.OrderAggregate;
using CourseApp.Order.Infrastructure;
using CourseApp.Shared.Dtos;
using MediatR;
using OrderAggregate = CourseApp.Order.Domain.OrderAggregate;

namespace CourseApp.Order.Application.Menu.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
{
    private readonly OrderDbContext _context;

    public CreateOrderCommandHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address{
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };

        OrderAggregate.Order newOrder = new(request.UserId, newAddress);

        request.OrderItems.ForEach(oi => {
            newOrder.AddOrderItem(oi.ProductId, oi.ProductName, oi.Price, oi.PictureUrl);
        });

        _context.Orders.Add(newOrder);

        await _context.SaveChangesAsync();

        return Response<CreatedOrderDto>.Success(new CreatedOrderDto{Id = newOrder.Id},201);
    }
}
