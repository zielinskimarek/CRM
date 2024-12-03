using CRM.Deals.Api;
using CRM.Deals.Domain;
using CRM.Deals.Domain.Aggregate;
using CRM.Deals.Domain.Commands;
using CRM.Deals.Domain.Models;
using CRM.Framework;
using CRM.Deals.Infrastructure.Persistence;
using CRM.Deals.Projections.Queries;
using Marten;
using MediatR;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IDealsRepository), typeof(DealsRepository));
builder.Services.AddSingleton<ICommandsQueue, CommandsQueue>();
builder.Services.AddHostedService<DealsHostedService>();
builder.Services.AddMediatR(config =>
{
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(DealAggregateLoader<,>));
    config.RegisterServicesFromAssembly(typeof(CreateDeal).Assembly);
    config.RegisterServicesFromAssembly(typeof(GetDeal).Assembly);
});
builder.Services.AddScoped<AggregateContext<Deal>>();
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