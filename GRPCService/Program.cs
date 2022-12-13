using GRPC.Bruger;
using Grpc.Net.Client;
using GRPCService.LogicImpl;
using Aktie = Domain.Models.Aktie;

using var channel = GrpcChannel.ForAddress("http://localhost:1337");
var client = new BrugerService.BrugerServiceClient(channel);


var helloRequest = new Bruger
{
    Username = "bigman5",
    Password = "biggerman3",
    DepotID = 400,
    Saldo = 1337.6969
};

var request = new BrugerRequest
{
    Bruger = { helloRequest },
    Param = "hello"
};

async Task<BrugerResponse> HelloTask()
{
    var helloResponse = await client.createBrugerAsync(helloRequest);
    Console.WriteLine(helloResponse.Response);
    return helloResponse;
}

async Task<Bruger> getBruger()
{
    var bruger = await client.getBrugerAsync(request);
    Console.WriteLine(bruger.Saldo);
    return bruger;
}

//BrugerResponse response = await HelloTask();
//Console.WriteLine(response.Response);
//Bruger bruger = await getBruger();
//Console.WriteLine(bruger.DepotID);
var logic = new BrugerLogic();
var aktie = new Aktie
{
    Navn = "TSLA",
    Firma = "Tesla",
    Pris = 4200,
    High = 4269,
    Low = 1337
};

var aktieLogic = new AktieLogic();
var depoter = await aktieLogic.getAllAktierFromDepot(6);
Console.WriteLine(depoter.Count);