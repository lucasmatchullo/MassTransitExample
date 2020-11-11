using System;
using System.Net.Http;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;

namespace ConsumidorService.Consumers
{
    public class TicketConsumer : IConsumer<Endereco>
    {
        private readonly ILogger<TicketConsumer> _logger;
        private  readonly  IPublishEndpoint _publishEndPoint;

        public TicketConsumer(ILogger<TicketConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndPoint = publishEndpoint;
        }
        public Task Consume(ConsumeContext<Endereco> context)
        {
            var data = context.Message;
            _logger.LogInformation("Mensagem recebida no ConsumidorService: ");
            try
            {
                _logger.LogInformation("Enviando callback!!");
                return _publishEndPoint.Publish(new CallBackMessage
                {
                    IdMessage = context.Message.Id,
                    Message = "Sucesso ao adicionar endereço!"
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}