using GRPC.Bruger;
using Aktie = Domain.Models.Aktie;

namespace GRPCService.LogicInterfaces;

public interface IAktieLogic
{
    Task<AktieResponse> updateAktie(Aktie aktie);
    Task<List<AktieResponse>> updateAktie(Aktie[] aktie);
    Task<AktieResponse> deleteAktie(Aktie aktie);
    Task<Aktie> getAktie(String name);
}