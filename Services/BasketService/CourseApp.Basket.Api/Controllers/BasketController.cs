using CourseApp.Basket.Api.Dtos;
using CourseApp.Basket.Api.Services;
using CourseApp.Shared;
using CourseApp.Shared.services;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Basket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : CustomBaseController
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _identityService;

    public BasketController(IBasketService basketService, ISharedIdentityService identityService)
    {
        _basketService = basketService;
        _identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        return CreateActionResult(await _basketService.GetBasket(_identityService.GetUserId));
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateBasket([FromBody] BasketDto basketDto)
    {
        var response = await _basketService.SaveOrUpdate(basketDto);
        return CreateActionResult(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
    {
        return CreateActionResult(await _basketService.Delete(_identityService.GetUserId));   
    }

}
