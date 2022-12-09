using Google.Protobuf.Collections;
using GRPC.Bruger;
using Grpc.Net.Client;
using GRPCService.LogicInterfaces;
using Newtonsoft.Json;
using Aktie = Domain.Models.Aktie;

namespace GRPCService.LogicImpl;

public class AktieLogic : IAktieLogic
{
    public AktieLogic()
    {
    }

    public async Task<AktieResponse> updateAktie(Aktie aktie)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        GRPC.Bruger.Aktie grpcAktie = new GRPC.Bruger.Aktie()
        {
            Navn = aktie.Navn,
            Firma = aktie.Firma,
            High = aktie.High,
            Pris = aktie.Pris,
            Low = aktie.Low
        };

        RepeatedField<GRPC.Bruger.Aktie> repeatedField = new RepeatedField<GRPC.Bruger.Aktie>();
        repeatedField.Capacity = 1;
        repeatedField.Insert(0, grpcAktie);

        AktieRequest request = new AktieRequest()
        {
            Aktie = { repeatedField },
            Antal = 1,
            Param = "update"
        };

        AktieResponse response = await client.handleAktieAsync(request);
        Console.WriteLine(response.Response);
        return response;
    }

    public async Task<List<AktieResponse>> updateAktie(Aktie[] aktie)
    {
        List<AktieResponse> responses = new List<AktieResponse>();

        for (int i = 0; i >= aktie.Length; i++)
        {
           responses[i] = await updateAktie(aktie[i]);
        }

        return responses;
    }

    public Task<AktieResponse> deleteAktie(Aktie aktie)
    {
        throw new NotImplementedException();
    }

    public async Task<Aktie> getAktie(string name)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        
        AktieName aktieName = new AktieName()
        {
            Name = name,
            
            
        };

        GRPC.Bruger.Aktie grpcAktie = await client.getAktieAsync(aktieName);
        Aktie aktie = new Aktie()
        {
            Firma = grpcAktie.Firma,
            High = grpcAktie.High,
            Low = grpcAktie.Low,
            Navn = grpcAktie.Navn,
            Pris = grpcAktie.Pris
        };
        return aktie;
    }

    public async Task<List<Aktie>> getAllAktier()
    {
        List<Aktie> aktier;

        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        getAllAktier getAllAktier = new getAllAktier()
        {
            Param = "get"
        };
        allAktier allAktier = await client.getAllAsync(getAllAktier);
        List<GRPC.Bruger.Aktie>? tempGRPC = JsonConvert.DeserializeObject<List<GRPC.Bruger.Aktie>>(allAktier.Aktier.ToString());

        aktier = new List<Aktie>(tempGRPC.Count);
        for (int i = 0; i < tempGRPC.Count; i++)
        {
            Aktie tempaktie = new Aktie()
            {
                Navn = tempGRPC[i].Navn,
                Firma = tempGRPC[i].Firma,
                Pris = tempGRPC[i].Pris,
                High = tempGRPC[i].High,
                Low = tempGRPC[i].Low,
            };
            aktier.Add(tempaktie);
        }
        return aktier;
    }


    public async Task<AktieResponse> buyAktie(int antal, int depotID, GRPC.Bruger.Aktie aktie)
    {
        var client = new BrugerService.BrugerServiceClient(GrpcChannel.ForAddress("http://localhost:1337"));
        AktieRequest aktieRequest = new AktieRequest()
        {
            Aktie = { aktie },
            Param = "buy",
            DepotID = depotID,
            Antal = antal
        };

        AktieResponse response = await client.handleAktieAsync(aktieRequest);

        return response;

    }
}