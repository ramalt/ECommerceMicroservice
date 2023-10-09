using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Order.Application.Dtos;

public class OrderItemDto
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string? PictureUrl { get; set; }
    public Decimal Price { get; set; }
}
