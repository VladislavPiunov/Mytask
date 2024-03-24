using Notifications.API.Extensions;
using Notifications.API.Model;
using Notifications.API.Repositories;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args);

var NotificationsSpecificOrigin = "_notificationsSpecificOrigin";

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(NotificationsSpecificOrigin,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Add Config Server
builder.Configuration.AddConfigServer();

// Add redis
builder.Services.AddRedisCache(builder.Configuration);

builder.Services.AddTransient<IRedisNotificationsRepository, RedisNotificationsRepository>();

//Add Eureka
builder.Services.AddServiceDiscovery(o => o.UseEureka());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
