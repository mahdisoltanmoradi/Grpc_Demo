using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Server.Protos;
using Polly;

namespace Grpc.Client.Repositories
{
    public static class DefaultRepository
    {
        public static async Task Hello()
        {
            // Creating a channel for communication between client and server
            using var channel = GrpcChannel.ForAddress("http://localhost:5007");
            var client = new Greeter.GreeterClient(channel);

            //-----------------Example MetaData--------------
            var headers = new Grpc.Core.Metadata();
            headers.Add("Agent", "User1");

            //---------------Create CancellationToken-----------
            var source = new CancellationTokenSource();
            var token = source.Token;
            source.CancelAfter(TimeSpan.FromSeconds(1));

            var maxRetryAttempts = 5;
            var pauseBetweenFailures = TimeSpan.FromSeconds(3);

            //------------------Retry Pollisy----------------------
            var retryPollisy = Policy
                .Handle<RpcException>()
                .WaitAndRetryAsync(maxRetryAttempts,
                i => pauseBetweenFailures, (ex, pause) =>
                {
                    Console.WriteLine(ex.Message + "=>" + pause.TotalSeconds);
                });

            await retryPollisy.ExecuteAsync(async () =>
            {
                var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "Mahdi" }, headers, DateTime.UtcNow.AddSeconds(2),token);

                Console.WriteLine(reply.Message);
            });
        }
    }
}
