using MicroserviceSample.PlatformService.Features.Platforms.Dtos;
using System.Text;
using System.Text.Json;

namespace MicroserviceSample.PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration) : ICommandDataClient
{
    private readonly HttpClient httpClient = httpClient;
    private readonly IConfiguration configuration = configuration;

    public async Task SendPlatformToCommand(PlatformCreateExternalDto platformCreateExternalDto)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(platformCreateExternalDto),
            Encoding.UTF8,
            "application/json");

        var response = await httpClient.PostAsync(configuration["CommandService"], httpContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to CommandService was OK!");
        }
        else
        {
            Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
        }
    }
}
