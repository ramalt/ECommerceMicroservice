using AutoMapper;
using CourseApp.Order.Application.Dtos;
using OrderAggregate = CourseApp.Order.Domain.OrderAggregate;

namespace CourseApp.Order.Application.Config;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<OrderAggregate.Order, OrderDto>().ReverseMap();
        CreateMap<OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderAggregate.Address, AddressDto>().ReverseMap();
    }
}
