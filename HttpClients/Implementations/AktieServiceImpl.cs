using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using GRPC.Bruger;
using HttpClients.ClientInterfaces;
using Aktie = Domain.Models.Aktie;

namespace HttpClients.Implementations;

public class AktieServiceImpl : IAktieService
{
    
    private readonly HttpClient client;

    public AktieServiceImpl(HttpClient client)
    {
        this.client = client;
    }
   

   

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
    
        HttpResponseMessage response = await client.GetAsync("/Aktie");
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

   public async Task buyAktie(int antal, int depotID, Aktie aktie)
    {
        AktieRequestDTO requestDto = new AktieRequestDTO()
        {
            antal = antal,
            depotID = depotID,
            aktie = aktie,
            param = "buy"

        };
            
        HttpResponseMessage response = await client.PostAsJsonAsync("/Aktie",requestDto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
    }
}