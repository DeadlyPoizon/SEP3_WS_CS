using Domain.Models;
using GRPC.Bruger;
using Aktie = Domain.Models.Aktie;
using Depot = Domain.Models.Depot;

namespace GRPCService.LogicInterfaces;

public interface IAktieLogic
{
    Task<AktieResponse> updateAktie(Aktie aktie);
    Task<List<AktieResponse>> updateAktie(Aktie[] aktie);
    Task<AktieResponse> deleteAktie(Aktie aktie);
    Task<Aktie> getAktie(string name);
    Task<List<Aktie>> getAllAktier();

    Task<AktieResponse> buyAktie(int antal, int depotID, Aktie aktie);

    Task<AktieResponse> sellAktie(int antal, int depotID, Aktie aktie);

    Task<List<Depot>> getAllAktierFromDepot(int depotID);

    Task<List<Transaktion>> getTransaktionerFraUsername(string username);


}