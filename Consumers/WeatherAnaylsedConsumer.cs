using MassTransit;
using MasstransitRabbitMq.Helper;
using MasstransitRabbitMq.Model.BusModels;

namespace MasstransitRabbitMq.Consumers
{
    public class WeatherAnalysedConsumer : IConsumer<WeatherAnalysed>
    {
        ILogger<WeatherAnalysedConsumer> _logger;
        public WeatherAnalysedConsumer(ILogger<WeatherAnalysedConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<WeatherAnalysed> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message.Date);
            try
            {
                context.Message.InvokeRedisCache();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}