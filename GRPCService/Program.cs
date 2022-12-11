using Domain.Models;
using GRPC.Bruger;
using Grpc.Net.Client;
using Grpc.Core;
using GRPCService.LogicImpl;
using Aktie = Domain.Models.Aktie;
using Bruger = GRPC.Bruger.Bruger;


using var channel = GrpcChannel.ForAddress("http://localhost:1337");
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

async Task<BrugerResponse> HelloTask()
{
    BrugerResponse helloResponse = await client.createBrugerAsync(helloRequest);
    Console.WriteLine(helloResponse.Response);
    return helloResponse;
}

async Task<Bruger> getBruger()
{
    Bruger bruger = await client.getBrugerAsync(request);
    Console.WriteLine(bruger.Saldo);
    return bruger;
}

//BrugerResponse response = await HelloTask();
//Console.WriteLine(response.Response);
//Bruger bruger = await getBruger();
//Console.WriteLine(bruger.DepotID);
BrugerLogic logic = new BrugerLogic();
GRPC.Bruger.Aktie aktie = new GRPC.Bruger.Aktie()
{
    Navn = "TSLA",
    Firma = "Tesla",
    Pris = 4200,
    High = 4269,
    Low = 1337
};
//AktieResponse aktieResponse = await aktieLogic.updateAktie(aktie);

BrugerResponse response = await logic.resetBruger(1);
Console.WriteLine(response.Response);
