using GRPC.Bruger;
using Grpc.Core;
using Grpc.Net.Client;
using GRPCService.LogicInterfaces;

namespace GRPCService.LogicImpl;

public class BrugerLogic : IBrugerLogic
{



    public async Task<BrugerResponse> CreateBruger(Domain.Models.Bruger bruger)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        Bruger newBruger = new Bruger()
        {
            Username = bruger.Username,
            Password = bruger.Password,
            DepotID = bruger.DepotID,
            Saldo = bruger.Saldo
        };
        
        BrugerResponse response = await client.createBrugerAsync(newBruger);
        return response;
    }
    
    public async Task<Domain.Models.Bruger> GetBruger(String username)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        GRPC.Bruger.Bruger bruger = new Bruger()
        {
            Username = username,
        };
        
        BrugerRequest request = new BrugerRequest()
        {
            Bruger = { bruger },
            Param = "get"
        };

        Bruger grpcBruger = client.getBruger(request);
        Domain.Models.Bruger returnBruger = new Domain.Models.Bruger()
        {
            Username = grpcBruger.Username,
            Password = grpcBruger.Password,
            Saldo = grpcBruger.Saldo,
            DepotID = grpcBruger.DepotID
        };
        
        return returnBruger;
    }
    
}