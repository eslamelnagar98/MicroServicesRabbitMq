using MicroRabbitMq.Banking.Data.Context;
using MicroRabbitMq.Banking.Domain.Interfaces;
using MicroRabbitMq.Banking.Domain.Models;

namespace MicroRabbitMq.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _bankingDbContext;

        public AccountRepository(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _bankingDbContext.Accounts;
        }
    }
}
