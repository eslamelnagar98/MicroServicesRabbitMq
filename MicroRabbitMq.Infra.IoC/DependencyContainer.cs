using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbitMq.Banking.Application.Interfaces;
using MicroRabbitMq.Banking.Application.Services;
using MicroRabbitMq.Banking.Data.Context;
using MicroRabbitMq.Banking.Data.Repository;
using MicroRabbitMq.Banking.Domain.CommandHandlers;
using MicroRabbitMq.Banking.Domain.Commands;
using MicroRabbitMq.Banking.Domain.Interfaces;
using MicroRabbitMq.Infra.Bus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MicroRabbitMq.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void Register(this IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();

            //Domain Banking Commands 
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            //Application Services 
            services.AddTransient<IAccountService, AccountService>();

            //Data Layer
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
        }

        public static void AddDbConnection<TEntity>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TEntity : DbContext
        {
            services.AddDbContext<TEntity>(options =>
             {
                 options.UseSqlServer(configuration.GetConnectionString($"{typeof(TEntity).Name}Connection"))
                        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                         LogLevel.Information)
                        .EnableSensitiveDataLogging();
             });
        }

        public static TEntity ApplyDbMigrations<TEntity>(
            this IServiceScope serviceScope)
            where TEntity : DbContext
        {
            return serviceScope.ServiceProvider.GetRequiredService<TEntity>();
        }

    }
}
