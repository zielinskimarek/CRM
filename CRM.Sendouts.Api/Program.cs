using CRM.Framework;
using CRM.Sendouts.Api;
using CRM.Sendouts.Domain;
using CRM.Sendouts.Domain.Aggregate;
using CRM.Sendouts.Domain.Commands;
using CRM.Sendouts.Domain.Models;
using CRM.Sendouts.Infrastructure.EventHandling;
using CRM.Sendouts.Infrastructure.Persistence;
using CRM.Sendouts.Infrastructure.Smtp;
using CRM.Sendouts.Projections.Queries;
using Marten;
using MediatR;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(ISendoutsRepository), typeof(SendoutsRepository));
builder.Services.AddSingleton<ICommandsQueue, CommandsQueue>();
builder.Services.AddHostedService<SendoutsHostedService>();
builder.Services.AddMediatR(config =>
{
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(SendoutAggregateLoader<,>));
    config.RegisterServicesFromAssembly(typeof(CreateSendout).Assembly);
    config.RegisterServicesFromAssembly(typeof(GetSendout).Assembly);
    config.RegisterServicesFromAssembly(typeof(SendoutScheduledHandles).Assembly);
});
builder.Services.AddScoped<AggregateContext<Sendout>>();
builder.Services.AddScoped<ISmtpClient, SmtpClient>();
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Marten")!);
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
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