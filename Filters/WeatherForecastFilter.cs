using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MasstransitRabbitMq.Model.BusModels;

namespace MasstransitRabbitMq.Filters
{
    public class WeatherForecastFilter : IAsyncResultFilter
    {
        private readonly IPublishEndpoint publishEndpoint;
        // private readonly IDistributedCache distributedCache;

        public WeatherForecastFilter(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            //before
            await next.Invoke().ConfigureAwait(false);
            // after
            if (context.Result is ObjectResult result)
            {
                WeatherForecast[]? weatherResult = (object)result.Value as WeatherForecast[];

                foreach (WeatherForecast weather in weatherResult)
                {
                    await this.publishEndpoint.Publish(new WeatherAnalysed
                    {
                        Date = weather.Date,
                        Summary = weather.Summary,
                        TemperatureC = weather.TemperatureC
                    });
                }
            }
        }
    }
}