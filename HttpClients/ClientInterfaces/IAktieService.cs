using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IAktieService
{
    

    Task<IEnumerable<Aktie>> Getaktie(string? aktie = null);
}