using MassTransit;
using MasstransitRabbitMq.Consumers;

namespace MasstransitRabbitMq
{
    public static class RabbitMqConfiguration
    {
        public static void AddMasstransitRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<WeatherAnalysedConsumer>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    //cfg.Host("rabbitmq://indis:123456@localhost:5672");

                    cfg.Host("172.19.144.1", "/", h =>
                    {
                        h.Username("indis");
                        h.Password("123456");
                    });

                    cfg.ReceiveEndpoint("weather-analyzed", e =>
                    {
                        e.ConfigureConsumer<WeatherAnalysedConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}