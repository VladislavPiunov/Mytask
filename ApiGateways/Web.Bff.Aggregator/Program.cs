using Gateway.API;
using Microsoft.Extensions.Options;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;
using Shared.Options;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddConfigServer();

var routes = "Routes.dev";

builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routes;
    options.FileOfSwaggerEndPoints = "ocelot.swagger-api";
});

builder.Services.Configure<DescriptionConfiguration>(builder.Configuration.GetSection("Description"));

builder.Services
    .AddOcelot(builder.Configuration)
    .AddEureka()
    .AddPolly();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddServiceDiscovery(o => o.UseEureka());

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
//app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors();

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;
});
app.UseOcelot().Wait();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
