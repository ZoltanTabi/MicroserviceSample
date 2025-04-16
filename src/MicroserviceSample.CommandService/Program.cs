using FluentValidation;
using MicroserviceSample.CommandService.Features.Commands;
using MicroserviceSample.CommandService.Features.Platforms;
using MicroserviceSample.CommandService.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDefaultPersistenceModule();

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCommandsEndpoints();
app.MapPlatformsEndpoints();

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();
