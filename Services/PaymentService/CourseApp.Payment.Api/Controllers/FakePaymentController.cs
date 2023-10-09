using CourseApp.Shared;
using CourseApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace PaymentSerice.CourseApp.Payment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FakePaymentController : CustomBaseController
{
    [HttpPost]
    public IActionResult ReceivePayment()
    {
        return CreateActionResult(Response<NoContent>.Success(200));
    }
}
