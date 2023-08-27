using AutoMapper;
using Grpc.Net.Client;
using PlatformManager.CommandService.Models;
using CommandService;
using System.Threading.Channels;

namespace PlatformManager.CommandService.SyncDataServices.Grpc;

public class PlatformDataClient : IPlatformDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public IEnumerable<Platform> ReturnAllPlatforms()
    {
        Console.WriteLine($"--> Calling Grpc service {_configuration["GrpcPlatform"]}");
        var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]!);
        Console.WriteLine(channel.ToString());
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        Console.WriteLine(client.ToString());
        var request = new GetAllRequest();
        Console.WriteLine(request.ToString());

        try
        {
            var reply = client.GetAllPlatforms(request);
            Console.WriteLine(reply.ToString());
            return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"--> could not call grpc server {ex.Message}");
            return null;
        }
    }
}
