using CourseApp.Basket.Api.Settings;
using Microsoft.Extensions.Options;

namespace CourseApp.Basket.Api;

public static class ServiceExtensions
{
    public static void configureRedis(this IServiceCollection services)
    {
        services.AddSingleton<RedisService>(service => 
        {
            var redisSettings = service.GetRequiredService<IOptions<RedisSettings>>().Value;
            var redis = new RedisService(redisSettings.Host, redisSettings.Port);

            redis.Connect();

            
            return redis;
        });

    }
}
