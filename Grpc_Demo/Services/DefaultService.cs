using Grpc.Core;
using Grpc.Server.Protos;

namespace Grpc.Server.Services
{
    public class DefaultService:Greeter.GreeterBase
    {
        private readonly ILogger<DefaultService> _logger;
        public DefaultService(ILogger<DefaultService> logger)
        {
            _logger = logger;
        }

        public async override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            await Task.Delay(1000);
            return new HelloReply
            {
                Message = "Hello from\n " + request.Name
            };
        }
    }
}
