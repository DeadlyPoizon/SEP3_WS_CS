using GRPC.Bruger;
using Grpc.Core;
using Grpc.Net.Client;
using GRPCService.LogicInterfaces;

namespace GRPCService.LogicImpl;

public class BrugerLogic : IBrugerLogic
{



    public Task<BrugerResponse> CreateBruger()
    {
        
        throw new NotImplementedException();
    }
}