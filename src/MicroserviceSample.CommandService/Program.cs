using FluentValidation;
using MicroserviceSample.CommandService.Features.Commands;
using MicroserviceSample.CommandService.Features.Platforms;
using MicroserviceSample.CommandService.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.Configure<CommandStoreDatabaseSettings>(
    builder.Configuration.GetSection("CommandStoreDatabase"));

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
