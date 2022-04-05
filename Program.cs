using MassTransit;
using MasstransitRabbitMq;
using MasstransitRabbitMq.Consumers;
using MasstransitRabbitMq.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<WeatherForecastFilter>();



builder.Services.AddMasstransitRabbitMq();
builder.Services.AddRedisCache();
builder.Services.AddJaeger();

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

