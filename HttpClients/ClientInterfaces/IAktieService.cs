using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IAktieService
{
    Task<IEnumerable<Aktie>> Getaktie(string? aktie = null);

    Task<List<Aktie>> GetAllAktier();

    Task buyAktie(int antal, int depotID, Aktie aktie);

    Task sellAktie(int antal, int depotID, Aktie aktie);

    Task<List<Depot>> GetDepot(int depotID);
}