using BuildingBlocks.Behaviours;
using CatalogAPI.Data;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddMarten(opts => {
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP Request pipeline
app.MapCarter();

app.UseExceptionHandler(
        exceptionHandlerApp => {
            exceptionHandlerApp.Run(async context =>
        {
            var exceptio = context.Features.Get<IExceptionHandlerFeature>().Error;
            if (exceptio == null)
                return;

            var exceptionDetails = new ProblemDetails {
                Title = exceptio.Message,
                Status = StatusCodes.Status500InternalServerError,
                Detail = exceptio.StackTrace
            };

            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exceptio, exceptio.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(exceptionDetails);
        });
    });

app.MapGet("/", () => "Hello World!");
app.UseHealthChecks("/healths", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse});

app.Run();
