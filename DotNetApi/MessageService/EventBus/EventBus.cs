using System;
using System.Text;
using MessageService.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageService.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public IModel Channel => _channel ?? (_channel = _connection.CreateModel());

        public EventBus(IConnectionFactory connectionFactory, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _connection = connectionFactory.CreateConnection();
        }

        public void Publish(IIntegrationEvent @event, string exchangeName)
        {
            CreateExchangeIfNotExist(exchangeName);

            var json = JsonConvert.SerializeObject(@event);
            var bytes = Encoding.UTF8.GetBytes(json);

            Channel.BasicPublish(exchangeName, string.Empty, body: bytes);
        }

        public void Subscribe<TH, TE>(string exchangeName, string subscriberName)
            where TH : IIntegrationEventHandler<TE>
            where TE : IIntegrationEvent
        {
            BindQueue(exchangeName, subscriberName);

            var consumer = new AsyncEventingBasicConsumer(Channel);

            consumer.Received += async (obj, args) =>
            {
                using (var scope =_serviceProvider.CreateScope())
                {
                    // Get service in runtime
                    var handler = scope.ServiceProvider.GetService<IIntegrationEventHandler<TE>>();

                    var jsonRequest = Encoding.UTF8.GetString(args.Body.ToArray());
                    var request = JsonConvert.DeserializeObject<TE>(jsonRequest);

                    await handler.HandleAsync(request);
                    
                    Channel.BasicAck(args.DeliveryTag, false);
                }
            };

            Channel.BasicConsume(subscriberName, false, consumer);
        }

        private void CreateExchangeIfNotExist(string exchangeName)
        {
            Channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true);
        }

        private void BindQueue(string exchangeName, string subscriberName)
        {
            CreateExchangeIfNotExist(exchangeName);

            Channel.QueueDeclare(subscriberName, true, false, autoDelete: false);
            Channel.QueueBind(subscriberName, exchangeName, string.Empty);
        }
    }
}