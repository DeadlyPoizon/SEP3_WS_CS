using GRPC.Bruger;
using Grpc.Net.Client;
using GRPCService.LogicInterfaces;
using Bruger = Domain.Models.Bruger;

namespace GRPCService.LogicImpl;

public class BrugerLogic : IBrugerLogic
{
    public async Task<BrugerResponse> CreateBruger(Bruger bruger)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        var newBruger = new GRPC.Bruger.Bruger
        {
            Username = bruger.Username,
            Password = bruger.Password,
            DepotID = bruger.DepotID,
            Saldo = bruger.Saldo
        };

        var response = await client.createBrugerAsync(newBruger);
        return response;
    }

    public async Task<Bruger> GetBruger(string username)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        var bruger = new GRPC.Bruger.Bruger
        {
            Username = username
        };

        var request = new BrugerRequest
        {
            Bruger = { bruger },
            Param = "get"
        };

        var grpcBruger = client.getBruger(request);
        var returnBruger = new Bruger
        {
            Username = grpcBruger.Username,
            Password = grpcBruger.Password,
            Saldo = grpcBruger.Saldo,
            DepotID = grpcBruger.DepotID
        };

        return returnBruger;
    }

    public async Task<BrugerResponse> resetBruger(int depotid)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        var bruger = new GRPC.Bruger.Bruger
        {
            DepotID = depotid
        };

        var request = new BrugerRequest
        {
            Bruger = { bruger },
            Param = "reset"
        };

        var response = client.handleBruger(request);

        return response;
    }
}