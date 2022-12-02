using GRPC.Bruger;

namespace GRPCService.LogicInterfaces;

public interface IBrugerLogic
{
    Task<BrugerResponse> CreateBruger();
    
}