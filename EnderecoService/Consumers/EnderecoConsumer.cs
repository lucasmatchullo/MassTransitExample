using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;

namespace EnderecoService.Consumers
{
    public class EnderecoConsumer : IConsumer<Endereco>
    {
        private readonly ILogger<EnderecoConsumer> _logger;
        private  readonly  IPublishEndpoint _publishEndPoint;
        private readonly FakeStore _fakeStore;

        public EnderecoConsumer(ILogger<EnderecoConsumer> logger, IPublishEndpoint publishEndpoint,
            FakeStore fakeStore)
        {
            _logger = logger;
            _publishEndPoint = publishEndpoint;
            _fakeStore = fakeStore;
        }
        public async Task Consume(ConsumeContext<Endereco> context)
        {
            _logger.LogInformation("Mensagem recebida no EnderecoService: ");
            var endereco = _fakeStore.Enderecos.SingleOrDefault(x => x.Id == context.Message.Id);
            if (endereco == null)
                await context.RespondAsync<EnderecoNotFound>(context.Message);
            else
                await context.RespondAsync<EnderecoStatusResult>(new
                {
                    EnderecoId = endereco.Id,
                    DateTime = DateTime.Now,
                    StatusCode = 200,
                    StatusText = "Sucesso, endereco existente!"
                });
        }
    }
}