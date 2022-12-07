using Google.Protobuf.Collections;
using GRPC.Bruger;
using Grpc.Net.Client;
using GRPCService.LogicInterfaces;
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

    public Task<AktieResponse> updateAktie(Aktie[] aktie)
    {
        throw new NotImplementedException();
    }

    public Task<AktieResponse> deleteAktie(Aktie aktie)
    {
        throw new NotImplementedException();
    }

    public Task<Aktie> getAktie(string name)
    {
        throw new NotImplementedException();
    }
}