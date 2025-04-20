using FluentValidation;
using MicroserviceSample.CommandService.AsyncDataServices;
using MicroserviceSample.CommandService.EventProcessing;
using MicroserviceSample.CommandService.Features.Commands;
using MicroserviceSample.CommandService.Features.Platforms;
using MicroserviceSample.CommandService.Persistance;
using MicroserviceSample.CommandService.SyncaDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.Configure<CommandStoreDatabaseSettings>(
    builder.Configuration.GetSection("CommandStoreDatabase"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDefaultPersistenceModule();

builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();

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

PrepDb.PrepPopulation(app);

app.Run();
