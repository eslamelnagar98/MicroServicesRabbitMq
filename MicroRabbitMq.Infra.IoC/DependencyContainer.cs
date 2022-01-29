using MicroRabbit.Domain.Core.Bus;
using MicroRabbitMq.Banking.Application.Interfaces;
using MicroRabbitMq.Banking.Application.Services;
using MicroRabbitMq.Banking.Data.Context;
using MicroRabbitMq.Banking.Data.Repository;
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

            //Application Services 
            services.AddTransient<IAccountService, AccountService>();

            //Data Layer
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
        }

        public static void AddbankingDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BankingDbContext>(options =>
             {
                 options.UseSqlServer(configuration.GetConnectionString("BankingDbConnection"))
                        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                         LogLevel.Information)
                        .EnableSensitiveDataLogging();
             });
        }
    }
}
