using CourseApp.Order.Application.Dtos;
using CourseApp.Order.Application.Menu.Commands;
using CourseApp.Order.Application.Menu.Queries;
using CourseApp.Shared;
using CourseApp.Shared.services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Order.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : CustomBaseController
{
    private readonly ISender _mediator;
    private readonly ISharedIdentityService _identityService;

    public OrderController(ISharedIdentityService identityService, IMediator mediator)
    {
        _identityService = identityService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var userId = _identityService.GetUserId;
        var response = await _mediator.Send(new GetOrdersByUserIdQuery{UserId = userId});

        return CreateActionResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrder([FromBody] CreateOrderCommand orderCommand)
    {
        var response = await _mediator.Send(orderCommand);
        return CreateActionResult(response);
    }


    








}
