using GRPC.Bruger;
using Grpc.Net.Client;
using Grpc.Core;


using var channel = GrpcChannel.ForAddress("http://localhost:6969");
    var client = new HelloService.HelloServiceClient(channel);

        HelloRequest helloRequest = new HelloRequest()
        {
            FirstName = "bigman",
            LastName = "biggerman"
        };
        
        async Task<HelloResponse> HelloTask()
    {
        HelloResponse helloResponse = await client.helloAsync(helloRequest);
        Console.WriteLine(helloResponse.Greeting);
        return helloResponse;
        
    }
        HelloResponse response = await HelloTask();
        Console.WriteLine(response.Greeting);

Console.WriteLine("hi");
