using GRPC.Bruger;
using Grpc.Net.Client;
using GRPCService.LogicInterfaces;

namespace GRPCService.LogicImpl;

public class BrugerLogic : IBrugerLogic
{

    public BrugerLogic()
    {
         var channel = GrpcChannel.ForAddress("http://localhost:1337");
        var client = new BrugerService.BrugerServiceClient(channel);
        
        Bruger helloRequest = new Bruger()
        {
            Username = "bigman5",
            Password = "biggerman3",
            DepotID = 400,
            Saldo = 1337.6969
        };

        BrugerRequest request = new BrugerRequest()
        {
            Bruger = { helloRequest },
            Param = "hello"
        };
    }
    

    public Task<BrugerResponse> CreateBruger()
    {
        throw new NotImplementedException();
    }
}