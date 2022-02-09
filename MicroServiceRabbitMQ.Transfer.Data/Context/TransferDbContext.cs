using MicroServiceRabbitMQ.MicroServices.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbitMq.MicroServices.Transfer.Data.Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions<TransferDbContext> options) : base(options)
        {
        }

        public DbSet<TransferLog> TransferLogs { get; set; }
    }
}
