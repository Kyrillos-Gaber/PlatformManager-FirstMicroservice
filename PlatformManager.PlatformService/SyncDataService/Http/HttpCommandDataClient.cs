using PlatformManager.PlatformService.Dto;
using System.Text;
using System.Text.Json;

namespace PlatformManager.PlatformService.SyncDataService.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        StringContent httpContent = new(
            JsonSerializer.Serialize(platform),
            Encoding.UTF8,
            "application/json");

        var uri = _configuration.GetValue<string>("CommandService");

        HttpResponseMessage response = await _httpClient.PostAsync(
            uri,
            httpContent);

        if (response.IsSuccessStatusCode)
            Console.WriteLine($"SUCCESS TO send sync {response.Content} => {uri}");
        else
            Console.WriteLine("FAILD  => :( ");
    }
}
