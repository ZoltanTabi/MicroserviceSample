using FluentValidation;
using MicroserviceSample.PlatformService.Features.Platforms;
using MicroserviceSample.PlatformService.Persistance;
using MicroserviceSample.PlatformService.SyncDataServices.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

if (builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SQL Server");
    Console.WriteLine($"--> Connectionstring: {builder.Configuration.GetConnectionString("PlatformsConn")}");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}
else
{
    Console.WriteLine("--> Using InMemoryDb for development");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMemoryDb"));
}

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDefaultPersistenceModule();

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

Console.WriteLine("CommandService URL: " + builder.Configuration["CommandService"]);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Register platform endpoints
app.MapPlatformEndpoints();

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();
