using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

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
        var uri = "/aktier";
        if (!string.IsNullOrEmpty(aktie)) uri += $"?aktie={aktie}";
        var response = await client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var aktier = JsonSerializer.Deserialize<IEnumerable<Aktie>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return aktier;
    }

    public async Task<List<Aktie>> GetAllAktier()
    {
        var response = await client.GetAsync("/Aktie/all");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var aktier = JsonSerializer.Deserialize<List<Aktie>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return aktier;
    }

    public async Task<List<Depot>> GetDepot(int depotID)
    {
        var response = await client.GetAsync($"/Aktie/depot?depotID={depotID}");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var aktier = JsonSerializer.Deserialize<List<Depot>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return aktier;
    }

    public async Task buyAktie(int antal, int depotID, Aktie aktie)
    {
        var requestDto = new AktieRequestDTO
        {
            antal = antal,
            depotID = depotID,
            aktie = aktie,
            param = "buy"
        };

        var response = await client.PostAsJsonAsync("/Aktie/buy", requestDto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }

    public async Task sellAktie(int antal, int depotID, Aktie aktie)
    {
        var requestDto = new AktieRequestDTO
        {
            antal = antal,
            depotID = depotID,
            aktie = aktie,
            param = "sell"
        };

        var response = await client.PostAsJsonAsync("/Aktie/sell", requestDto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }
    
    public async Task<List<Transaktion>> GetTransaktioner(string username)
    {
        var response = await client.GetAsync($"/Transaktion?username={username}");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        List<Transaktion> transaktioner = JsonSerializer.Deserialize<List<Transaktion>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return transaktioner;
    }
}