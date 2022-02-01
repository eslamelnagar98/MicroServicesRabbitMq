using MicroRabbitMq.Banking.Application.Models;
using MicroRabbitMq.Banking.Domain.Models;
namespace MicroRabbitMq.Banking.Application.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Transfer(AccountTransfer accountTransfer);
    }
}
 