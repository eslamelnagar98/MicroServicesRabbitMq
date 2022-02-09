using MicroRabbitMq.MicroServices.Transfer.Data.Context;
using MicroServiceRabbitMQ.MicroServices.Transfer.Domain.Models;
using MicroServiceRabbitMQ.Transfer.Domain.Interfaces;

namespace MicroServiceRabbitMQ.Transfer.Data.Repository
{
    public class TransferRepository: ITransferRepository
    {
        private TransferDbContext _transferDbContext;

        public TransferRepository(TransferDbContext transferDbContext)
        {
            _transferDbContext = transferDbContext;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _transferDbContext.TransferLogs;
        }
    }
}
