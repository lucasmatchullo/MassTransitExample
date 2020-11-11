using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace ProdutorService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;
        private readonly ILogger<EnderecoController> _logger;

        public EnderecoController(IBus bus, ILogger<EnderecoController> logger,
            IPublishEndpoint publishEndpoint)
        {
            _bus = bus;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> AddEndereco(Endereco endereco)
        {
            if (endereco != null)
            {
                // Envio direto da mensagem para um microserviço, com resposta capturada Consumer
                
                // Uri uri = new Uri("queue:enderecoService");
                // var endPoint = await _bus.GetSendEndpoint(uri);
                // await endPoint.Send<Endereco>(endereco, context =>
                //     context.CorrelationId = context.Message.Id);
                
                // Publicação da mensagem para vários micro serviços
                
                // await _publishEndpoint.Publish(endereco);
                
                // Envio de uma mensagem ditera para um micro serviço, recebendo um resposta no proprio controler
                
                var serviceEndereco = new Uri("queue:enderecoService");
                var clientEndereco = _bus.CreateRequestClient<Endereco>(serviceEndereco);
                var (statusResponse, enderecoNotFound) = await clientEndereco.GetResponse<EnderecoStatusResult, EnderecoNotFound>(endereco);
                if (statusResponse.IsCompletedSuccessfully)
                {
                    // var statusSucess = await statusResponse;
                    _logger.LogInformation("Resposta EnderecoService: " + statusResponse.Result.Message.StatusText);
                }
                else
                {
                    var notFound = await enderecoNotFound;
                    _logger.LogInformation("Resposta EnderecoService: NotFound");
                }
                
                return Ok(endereco);
            }
            return BadRequest();
        }
    }
}