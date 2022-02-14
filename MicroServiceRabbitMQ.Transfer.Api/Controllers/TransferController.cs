using MicroServiceRabbitMQ.MicroServices.Transfer.Domain.Models;
using MicroServiceRabbitMQ.Transfer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceRabbitMQ.Transfer.Api.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController:ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpGet]
        public ActionResult<List<TransferLog>> Get()
        {

            return Ok(_transferService.GetTransferLogs());
        }
    }
}
