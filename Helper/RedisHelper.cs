using System.Text;
using MasstransitRabbitMq.Model.BusModels;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MasstransitRabbitMq.Helper
{
    public static class RedisHelper
    {
        public static void InvokeRedisCache(this WeatherAnalysed message, IDistributedCache distributedCache)
        {
            try
            {
                Console.WriteLine($"Redis {DateTime.Now.ToShortDateString()} de güncellendi");
                var serializedWeather = JsonSerializer.Serialize(message.Weathers);
                var weatherEncoded = Encoding.UTF8.GetBytes(serializedWeather);
                distributedCache.Set("weather", weatherEncoded);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}