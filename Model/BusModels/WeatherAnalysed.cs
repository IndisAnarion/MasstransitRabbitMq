namespace MasstransitRabbitMq.Model.BusModels
{
    public class WeatherAnalysed
    {
        public DateTime Date => DateTime.Now;

        public List<Weather>? Weathers { get; set; }
    }
}