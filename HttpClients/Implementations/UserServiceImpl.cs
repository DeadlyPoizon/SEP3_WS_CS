using System.Net.Http.Json;
using System.Text.Json;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserServiceImpl : IUserService
{
    private readonly HttpClient client;

    public UserServiceImpl(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Bruger> Create(Bruger bruger)
    {
        var response = await client.PostAsJsonAsync("/User", bruger);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var create = JsonSerializer.Deserialize<Bruger>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return create;
    }

    public async Task resetBruger(int depotID)
    {
        var response = await client.DeleteAsync($"User/{depotID}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}