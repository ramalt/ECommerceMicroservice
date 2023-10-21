using CourseApp.Discount.Api.Services;
using CourseApp.Shared;
using CourseApp.Shared.services;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Discount.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountController : CustomBaseController
{
    private readonly IDiscountService _discService;
    private readonly ISharedIdentityService _identityService;

    public DiscountController(ISharedIdentityService identityService, IDiscountService discService)
    {
        _identityService = identityService;
        _discService = discService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDiscounts(){
        return CreateActionResult(await _discService.GetAll());

    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDiscountById(int id){
        return CreateActionResult(await _discService.GetById(id));

    }

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetDiscountByCode(string code){

        var userId = _identityService.GetUserId;
        var discount = await _discService.GetByCodeAndUserId(code, userId);
        return CreateActionResult(discount);
        
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscount([FromBody] Models.Discount discount){
        return CreateActionResult(await _discService.Save(discount));

    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscount([FromBody] Models.Discount discount){
        return CreateActionResult(await _discService.Update(discount));

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscount(int id){
        return CreateActionResult(await _discService.Delete(id));

    }










}
