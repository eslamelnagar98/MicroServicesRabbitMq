using MicroRabbit.Domain.Core.Bus;
using MicroRabbitMq.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbitMq.Infra.IoC
{
    public class DependencyContainer
    {
        public static void Register(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
