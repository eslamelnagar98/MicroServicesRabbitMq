using MicroServiceRabbitMQ.MicroServices.Transfer.Domain.Models;

namespace MicroServiceRabbitMQ.Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
    }
}
