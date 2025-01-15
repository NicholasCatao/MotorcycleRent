using RentMotorBike.Worker.BackGroundServices.Interfaces;
using System.Text.Json;

namespace RentMotorBike.Workers.BackGroundServices;

public class SnsNotification(IHttpClientFactory httpClientFactory) : IMailService
{
    private readonly HttpClient _httpClientFactory = httpClientFactory.CreateClient("namedclient");
    private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    public async Task<bool> SendAsync<T>(T from)
    {
        using var response = await _httpClientFactory.PostAsync("/from", new StringContent(JsonSerializer.Serialize(from)));
       
        response.EnsureSuccessStatusCode();


        //return response.IsSuccessStatusCode;
        return false;

    }

}