using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MicroRabbitMq.Infra.Bus
{
    public class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        const string Url = "amqp://Islam:12345@192.168.1.3:5672/";


        public RabbitMQBus(IMediator mediator)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            CreateChannel(channel =>
            {
                var eventName = typeof(T).Name;
                channel.QueueDeclare(queue: eventName, durable: false,
                                                     exclusive: false, autoDelete: false,
                                                     arguments: null);

                var message = JsonSerializer.Serialize(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: eventName, basicProperties: null, body: body);
            });
        }

        public void Subscribe<T, THandler>()
            where T : Event
            where THandler : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(THandler);

            if (!_eventTypes.Any(type => type.GetType() == typeof(T)))
                _eventTypes.Add(typeof(T));

            if (!_handlers.TryGetValue(eventName, out _))
                _handlers.Add(eventName, new List<Type>());

            if (_handlers[eventName].Any(type => type.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} ALready Is Register For {eventName}");
            }

            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            CreateChannel(channel =>
            {
                var eventName = typeof(T).Name;
                channel.QueueDeclarePassive(queue: eventName);
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += ConsumerRecived;
                channel.BasicConsume(queue: eventName, autoAck: true, consumer);
            }, true);
        }



        private void CreateChannel(Action<IModel> handlerEvent, bool consumeAsync = default)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(Url),
                DispatchConsumersAsync = consumeAsync
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                handlerEvent?.Invoke(channel);
            }
        }


        private async Task ConsumerRecived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Task ProcessEvent(string eventName, string message)
        {
            throw new NotImplementedException();
        }


    }
}
