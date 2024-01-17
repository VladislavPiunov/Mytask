using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mytask.API.Rabbit
{
    public class RabbitConnectionHelper
    {
        public string RoutingKey { get; } = "/";

        private ConnectionFactory _factory { get; set; }

        private readonly ILogger<RabbitConnectionHelper> _logger;

        private readonly RabbitMqConfiguration _rabbitConf;

        public RabbitConnectionHelper(
            ILogger<RabbitConnectionHelper> logger,
            IOptions<RabbitMqConfiguration> rabbitConf)
        {
            _logger = logger;
            _rabbitConf = rabbitConf.Value ?? throw new ArgumentNullException(nameof(RabbitMqConfiguration));
        }

        public ConnectionFactory GetConnection()
        {
            _factory ??= new ConnectionFactory
            {
                HostName = _rabbitConf.Host,
                UserName = _rabbitConf.Username,
                Password = _rabbitConf.Password,
                Port = _rabbitConf.Port
            };

            return _factory;
        }

        public void PackAndSendMessage(string exchange, string queueName, string message)
        {
            try
            {
                var factory = GetConnection();
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchange, routingKey: RoutingKey, basicProperties: properties, body: body);

                _logger.LogInformation($"{nameof(PackAndSendMessage)} for queue '{queueName}' Sent '{message}' at {DateTimeOffset.Now}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
