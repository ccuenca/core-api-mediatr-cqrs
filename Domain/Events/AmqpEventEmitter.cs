using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using TestMediaTR.Support;

namespace TestMediaTR.Domain.Events
{
    public class AmqpEventEmitter : IEventEmitter
    {
        private readonly AmqpOptions amqpOptions;
        private readonly ConnectionFactory connectionFactory;

        public AmqpEventEmitter(IOptions<AmqpOptions> options)
        {
            amqpOptions = options.Value;

            connectionFactory = new ConnectionFactory
            {
                UserName = amqpOptions.UserName,
                Password = amqpOptions.Password,
                VirtualHost = amqpOptions.VirtualHost,
                HostName = amqpOptions.HostName,
                Uri = new Uri(amqpOptions.Uri)
            };
        }

        public void EmitConceptoCreatedEvent(ConceptoCreatedEvent message)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: Constants.ConceptosQueue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var jsonPayload = message.ToJson();
                    var body = Encoding.UTF8.GetBytes(jsonPayload);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: Constants.ConceptosQueue,
                        basicProperties: null,
                        body: body);
                }
            }
        }
    }
}