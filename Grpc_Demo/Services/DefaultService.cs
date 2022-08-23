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
            await Task.Delay(100);
            return new HelloReply
            {
                Message = "Hello from\n " + request.Name
            };
        }

        public override async Task<HelloReplyList> SayHelloList(HelloRequest request, ServerCallContext context)
        {
            var list = new HelloReplyList();
            await Task.Delay(100);
            for (int i = 0; i < 10; i++)
            {
                list.List.Add(new HelloReply
                {
                    Message = "Hello MahdiSoltanmoradi"+i.ToString()
                });
            }
            return list;
        }
    }
}
