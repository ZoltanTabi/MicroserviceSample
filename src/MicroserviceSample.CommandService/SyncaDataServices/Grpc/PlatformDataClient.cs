using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.PlatformService;

namespace MicroserviceSample.CommandService.SyncaDataServices.Grpc;

public class PlatformDataClient(IConfiguration configuration, IMapper mapper) : IPlatformDataClient
{
    private readonly IConfiguration configuration = configuration;
    private readonly IMapper mapper = mapper;

    public IEnumerable<Platform> ReturnAllPlatforms()
    {
        Console.WriteLine($"--> Calling GRPC Service {configuration["GrpcPlatform"]}");

        var channel = GrpcChannel.ForAddress(configuration["GrpcPlatform"]!);

        var client = new GrpcPlatform.GrpcPlatformClient(channel);

        var request = new GetAllRequest();

        try
        {
            var response = client.GetAllPlatforms(request);

            return mapper.Map<List<Platform>>(response.Platforms);
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"--> GRPC Error: {ex.Message}");
        }
        finally
        {
            channel.ShutdownAsync().Wait();
        }

        return [];
    }
}
