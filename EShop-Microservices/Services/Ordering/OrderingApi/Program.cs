using Ordering.Application;
using Ordering.Infrastructure;
using OrderingApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.Run();
