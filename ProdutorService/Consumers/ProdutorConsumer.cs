using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;

namespace ProdutorService.Consumers
{
    public class ProdutorConsumer : IConsumer<CallBackMessage>
    {
        private readonly IPublishEndpoint _publishEndPoint;
        private readonly ILogger<ProdutorConsumer> _logger;

        public ProdutorConsumer(IPublishEndpoint publishEndpoint, ILogger<ProdutorConsumer> logger)
        {
            _publishEndPoint = publishEndpoint;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CallBackMessage> context)
        {
            _logger.LogInformation("Consumindo no ProdutorService");
            _logger.LogInformation("Mensagem: " + context.Message.Message);
            return Task.CompletedTask;
        }
    }
}