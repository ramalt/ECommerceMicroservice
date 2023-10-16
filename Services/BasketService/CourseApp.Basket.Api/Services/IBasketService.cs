using CourseApp.Basket.Api.Dtos;
using CourseApp.Shared.Dtos;

namespace CourseApp.Basket.Api.Services;

public interface IBasketService
{
    Task<Response<BasketDto>> GetBasket(string userId);
    Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
    Task<Response<bool>> Delete(string userId);
}
