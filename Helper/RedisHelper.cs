using MasstransitRabbitMq.Model.BusModels;

namespace MasstransitRabbitMq.Helper
{
    public static class RedisHelper
    {
        public static void InvokeRedisCache(this WeatherAnalysed message)
        {
            try
            {
                Console.WriteLine($"Redis {message.Date.ToShortDateString()} de güncellendi");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}