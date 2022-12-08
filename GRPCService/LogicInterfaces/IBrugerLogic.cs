using GRPC.Bruger;
using Bruger = Domain.Models.Bruger;

namespace GRPCService.LogicInterfaces;

public interface IBrugerLogic
{
    Task<BrugerResponse> CreateBruger();

    Task<Bruger> GetBruger(String username);

}