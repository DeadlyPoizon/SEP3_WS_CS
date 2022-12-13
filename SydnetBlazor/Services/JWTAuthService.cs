using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using GRPCService.LogicImpl;
using GRPCService.LogicInterfaces;

namespace SydnetBlazor.Services;

public class JWTAuthService : IAuthService
{
    private readonly IBrugerLogic brugerLogic = new BrugerLogic();
    private readonly HttpClient client = new();


    // this private variable for simple caching
    public static string? Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }

    public async Task<Bruger> ValidateUser(string username, string password)
    {
        var existingUser = await brugerLogic.GetBruger(username);

        if (existingUser == null) throw new Exception("User not found");

        if (!existingUser.Password.Equals(password)) throw new Exception("Password mismatch");
        Console.WriteLine(existingUser.Username);
        return existingUser;
    }


    public async Task LoginAsync(string username, string password)
    {
        UserLoginDTO userLoginDto = new()
        {
            Username = username,
            Password = password
        };

        var userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:7064/auth/login", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new Exception(responseContent);

        var token = responseContent;
        Jwt = token;

        var principal = CreateClaimsPrincipal();

        OnAuthStateChanged.Invoke(principal);
    }

    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        var principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt)) return new ClaimsPrincipal();

        var claims = ParseClaimsFromJwt(Jwt);

        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
}