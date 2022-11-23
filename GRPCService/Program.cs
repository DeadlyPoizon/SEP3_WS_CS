using GRPC.Bruger;
using Grpc.Net.Client;
using Grpc.Core;


using var channel = GrpcChannel.ForAddress("http://localhost:6969");
    var client = new BrugerService.BrugerServiceClient(channel);


        Bruger helloRequest = new Bruger()
        {
            Username = "bigman",
            Password = "biggerman",
        };
        
        async Task<BrugerResponse> HelloTask()
    {
        BrugerResponse helloResponse = await client.createBrugerAsync(helloRequest);
        Console.WriteLine(helloResponse.Response);
        return helloResponse;
        
    }
        BrugerResponse response = await HelloTask();
        Console.WriteLine(response.Response);
        