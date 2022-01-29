using MicroRabbitMq.Banking.Domain.Models;

namespace MicroRabbitMq.Banking.Application.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
    }
}
