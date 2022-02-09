using MicroServiceRabbitMQ.MicroServices.Transfer.Domain.Models;
namespace MicroServiceRabbitMQ.Transfer.Application.Interfaces
{
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTransferLogs();
    }
}
