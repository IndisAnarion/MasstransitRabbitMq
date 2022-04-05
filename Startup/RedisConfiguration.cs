using MassTransit;
using MasstransitRabbitMq.Consumers;

namespace MasstransitRabbitMq
{
    public static class RedisConfiguration
    {
        public static void AddRedisCache(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "172.19.144.1:6379";
            });
        }
    }
}