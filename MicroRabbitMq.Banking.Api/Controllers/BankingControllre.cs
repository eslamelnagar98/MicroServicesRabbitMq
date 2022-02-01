using MicroRabbitMq.Banking.Application.Interfaces;
using MicroRabbitMq.Banking.Application.Models;
using MicroRabbitMq.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbitMq.Banking.Api.Controllers
{
    [Route("api/banking")]
    [ApiController]
    public class BankingControllre : ControllerBase
    {
        private readonly IAccountService _accountService;

        public BankingControllre(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return Ok(_accountService.GetAccounts());
        }

        [HttpPost]

        public IActionResult Post([FromBody] AccountTransfer accountTransfer)
        {
            _accountService.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }
    }
}
