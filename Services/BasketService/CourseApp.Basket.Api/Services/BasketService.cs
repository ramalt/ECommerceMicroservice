using System.Text.Json;
using CourseApp.Basket.Api.Dtos;
using CourseApp.Basket.Api.Settings;
using CourseApp.Shared.Dtos;

namespace CourseApp.Basket.Api.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redis;

    public BasketService(RedisService redis)
    {
        _redis = redis;
    }

    public async  Task<Response<bool>> Delete(string userId)
    {
        var status = await _redis.GetDb().KeyDeleteAsync(userId);
        return status ? Response<bool>.Success(204) : Response<bool>.Fail(400, "Basket could not found.");

    }

    public async Task<Response<BasketDto>> GetBasket(string userId)
    {
        var existBasket = await _redis.GetDb().StringGetAsync(userId);
        if(string.IsNullOrEmpty(existBasket))
            return Response<BasketDto>.Fail(404, "Basket not found");

        var basketDto = JsonSerializer.Deserialize<BasketDto>(existBasket);

        return Response<BasketDto>.Success(basketDto, 200);
     }

    public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
    {
        var status = await _redis.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

        return status ? Response<bool>.Success(204) : Response<bool>.Fail(500, "Basket could not update");
    }
}
