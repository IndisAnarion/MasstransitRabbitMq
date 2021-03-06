using MassTransit;
using MasstransitRabbitMq.Consumers;
using MasstransitRabbitMq.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<WeatherForecastFilter>();



builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<WeatherAnalysedConsumer>();
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        //cfg.Host("rabbitmq://indis:123456@localhost:5672");

        cfg.Host("172.24.160.1", "/", h=>{
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

builder.Services.AddStackExchangeRedisCache(options =>
 {
     options.Configuration = "172.24.160.1:6379";
    // options.InstanceName = "test-redis";
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

