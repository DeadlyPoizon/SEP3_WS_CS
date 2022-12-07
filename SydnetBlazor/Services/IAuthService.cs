using System.Security.Claims;
using Domain.Models;

namespace SydnetBlazor.Services;

public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    
    Task<Bruger> ValidateUser(string username, string password);
}