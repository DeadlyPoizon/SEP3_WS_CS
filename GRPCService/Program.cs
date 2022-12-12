using Domain.Models;
using GRPC.Bruger;
using Grpc.Net.Client;
using Grpc.Core;
using GRPCService.LogicImpl;
using Aktie = Domain.Models.Aktie;
using Bruger = GRPC.Bruger.Bruger;
using Depot = Domain.Models.Depot;


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
Aktie aktie = new Aktie()
{
    Navn = "TSLA",
    Firma = "Tesla",
    Pris = 4200,
    High = 4269,
    Low = 1337
};

AktieLogic aktieLogic = new AktieLogic();
List<Domain.Models.Depot> depoter = await aktieLogic.getAllAktierFromDepot(6);
Console.WriteLine(depoter.Count);
