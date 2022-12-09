using GRPC.Bruger;
using Aktie = Domain.Models.Aktie;

namespace HttpClients.ClientInterfaces;

public interface IAktieService
{
    

    Task<IEnumerable<Aktie>> Getaktie(string? aktie = null);
    
    Task<List<Aktie>> GetAllAktier();
    
    Task buyAktie(int antal, int depotID, GRPC.Bruger.Aktie aktie);
}