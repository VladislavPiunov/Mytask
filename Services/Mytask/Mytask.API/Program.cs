using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using MongoDB.Driver;
using Mytask.API.Extensions.Auth;
using Mytask.API.Model;
using Mytask.API.Repositories;
using Mytask.API.Services;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();

// Add Config Server
builder.Configuration.AddConfigServer();
builder.Services.Configure<EnvireConfiguration>(builder.Configuration.GetSection("MytaskAPI"));

// Add KeyCloak
builder.Services.AddKeycloakAuthentication(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MongoDb
var provider = builder.Services.BuildServiceProvider();
var options = provider.GetRequiredService<IOptions<EnvireConfiguration>>().Value;
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
    app.UseSwaggerUI();
// }

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseRouting();

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();