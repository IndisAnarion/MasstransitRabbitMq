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
                List<Weather> weatherAnalyzedResults = new List<Weather>();
                if (weatherResult != null && weatherResult.Any())
                {
                    weatherResult.ToList().ForEach(w =>
                    {
                        Weather weather = new Weather
                        {
                            Date = w.Date,
                            Summary = w.Summary,
                            TemperatureC = w.TemperatureC
                        };
                        weatherAnalyzedResults.Add(weather);
                    });

                    await this.publishEndpoint.Publish(new WeatherAnalysed { Weathers = weatherAnalyzedResults });
                }
            }
        }
    }
}