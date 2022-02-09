using MicroRabbit.Domain.Core.Bus;
using MicroServiceRabbitMQ.MicroServices.Transfer.Domain.Models;
using MicroServiceRabbitMQ.Transfer.Application.Interfaces;
using MicroServiceRabbitMQ.Transfer.Domain.Interfaces;

namespace MicroServiceRabbitMQ.Transfer.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IEventBus _bus;

        public TransferService(ITransferRepository transferRepository, IEventBus bus)
        {
            _transferRepository = transferRepository;
            _bus = bus;
        }
     

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            throw new NotImplementedException();
        }

       
    }
}
