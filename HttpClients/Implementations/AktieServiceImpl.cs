using System.Text.Json;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class AktieServiceImpl : IAktieService
{
    
    private readonly HttpClient client;
   

   

    public async Task<IEnumerable<Aktie>> Getaktie(string? aktie)
    {
        string uri = "/aktier";
        if (!string.IsNullOrEmpty(aktie))
        {
            uri += $"?aktie={aktie}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Aktie> aktier = JsonSerializer.Deserialize<IEnumerable<Aktie>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return aktier;
    }

    public async Task<List<Aktie>> GetAllAktier()
    {
        string uri = "/Aktie";
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        List<Aktie> aktier = JsonSerializer.Deserialize<List<Aktie>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return aktier;
    }
}