using System.Security.Claims;
using Domain.Models;

namespace SydnetBlazor.Services;

public interface IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    Task<Bruger> ValidateUser(string username, string password);
}