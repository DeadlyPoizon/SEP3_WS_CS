using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
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
        HttpResponseMessage response = await client.PostAsJsonAsync("/User", bruger);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Bruger create = JsonSerializer.Deserialize<Bruger>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return create;
    
    }
}