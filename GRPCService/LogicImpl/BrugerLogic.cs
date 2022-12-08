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

    public async Task<Domain.Models.Bruger> GetBruger(String username)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        
        BrugerRequest request = new BrugerRequest()
        {
            Bruger = {  }
        };

        Bruger grpcBruger = client.getBruger(request);
        Domain.Models.Bruger bruger = new Domain.Models.Bruger()
        {
            Username = grpcBruger.Username,
            Password = grpcBruger.Password,
            DepotID = grpcBruger.DepotID,
            Saldo = grpcBruger.Saldo
        };
        return bruger;
    }
}