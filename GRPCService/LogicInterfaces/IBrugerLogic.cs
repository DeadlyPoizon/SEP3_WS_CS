using GRPC.Bruger;
using Bruger = Domain.Models.Bruger;

namespace GRPCService.LogicInterfaces;

public interface IBrugerLogic
{
    Task<BrugerResponse> CreateBruger(Bruger bruger);

    Task<Bruger> GetBruger(string username);


    Task<BrugerResponse> resetBruger(int depotid);
}