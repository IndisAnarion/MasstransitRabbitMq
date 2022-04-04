using MassTransit;
using MasstransitRabbitMq.Helper;
using MasstransitRabbitMq.Model.BusModels;
using Microsoft.Extensions.Caching.Distributed;

namespace MasstransitRabbitMq.Consumers
{
    public class WeatherAnalysedConsumer : IConsumer<WeatherAnalysed>
    {
        ILogger<WeatherAnalysedConsumer> logger;
        IDistributedCache distributedCache;

        public WeatherAnalysedConsumer(ILogger<WeatherAnalysedConsumer> logger, IDistributedCache distributedCache)
        {
            this.logger = logger;
            this.distributedCache = distributedCache;
        }

        public async Task Consume(ConsumeContext<WeatherAnalysed> context)
        {
            logger.LogInformation("Value: {Value}", context.Message.Date);
            try
            {
                context.Message.InvokeRedisCache(distributedCache);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}