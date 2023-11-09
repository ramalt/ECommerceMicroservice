using CourseApp.Payment.Api.Models;
using CourseApp.Shared;
using CourseApp.Shared.Dtos;
using CourseApp.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Payment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FakePaymentController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            //paymentDto ödeme işlemi 
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand
            {
                BuyerId = paymentDto.Order.BuyerId,
                Province = paymentDto.Order.Address.Province,
                District = paymentDto.Order.Address.District,
                Street = paymentDto.Order.Address.Street,
                Line = paymentDto.Order.Address.Line,
                ZipCode = paymentDto.Order.Address.ZipCode
            };

            paymentDto.Order.OrderItems.ForEach(x =>
                {
                    createOrderMessageCommand.OrderItems.Add(new OrderItem
                    {
                        PictureUrl = x.PictureUrl,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        ProductName = x.ProductName
                    });
                });

            await sendEndpoint.Send(createOrderMessageCommand);

            return CreateActionResult(Shared.Dtos.Response<NoContent>.Success(200));
        }
    }

}
