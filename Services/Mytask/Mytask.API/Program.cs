using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mytask.API.Extensions.Auth;
using Mytask.API.Model;
using Mytask.API.Repositories;
using Mytask.API.Services;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery.Client;
using Microsoft.OpenApi.Models;
using Mytask.API.Extensions.Options;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

var MytaskSpecificOrigin = "_mytaskSpecificOrigin";

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy(MytaskSpecificOrigin,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Add Config Server
builder.Configuration.AddConfigServer();
builder.Services.Configure<ConnectionsConfiguration>(builder.Configuration.GetSection("MytaskAPI"));

// Add KeyCloak
builder.Services.AddKeycloakAuthentication(builder.Configuration);

//Add Eureka
builder.Services.AddServiceDiscovery(o => o.UseEureka());

//Add RabbitMQ
//builder.Services.AddRabbitBase(builder.Configuration);

//builder.Services.AddHostedService<DeleteBoardReceiver>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "mytask",
    });
});

// Add MongoDb
var provider = builder.Services.BuildServiceProvider();
var options = provider.GetRequiredService<IOptions<ConnectionsConfiguration>>().Value;
builder.Services.AddHealthChecks().AddMongoDb(options.ConnectionStrings.Mongo);
builder.Services.AddSingleton(new MongoClient(options.ConnectionStrings.Mongo));

builder.Services.AddTransient<IBoardRepository, BoardRepository>();
builder.Services.AddTransient<IStageRepository, StageRepository>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //options.RoutePrefix = string.Empty;
});
// }

app.UseCors(MytaskSpecificOrigin);

app.UseRouting();

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();