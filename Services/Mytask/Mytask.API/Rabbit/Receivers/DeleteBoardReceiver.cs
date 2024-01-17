using MongoDB.Driver;
using Mytask.API.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace Mytask.API.Rabbit.Receivers
{
    public class DeleteBoardReceiver : BackgroundService
    {
        private readonly ILogger<DeleteBoardReceiver> _logger;
        private readonly RabbitConnectionHelper _connectionHelper;

        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly string _queue = "delete-board-queue";

        private readonly IServiceProvider _serviceProvider;

        public DeleteBoardReceiver(
            ILogger<DeleteBoardReceiver> logger,
            RabbitConnectionHelper connectionHelper,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _connectionHelper = connectionHelper;

            _factory = connectionHelper.GetConnection();
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine($"{nameof(DeleteBoardReceiver)} received {message}");

                        try
                        {
                            using var scope = _serviceProvider.CreateScope();
                            var mongoClient = scope.ServiceProvider.GetRequiredService<MongoClient>();
                            var database = mongoClient.GetDatabase("mytask");

                            var deleted = database.GetCollection<Board>("boards")
                                .FindOneAndDelete(b => b.Id == message);
                            if (deleted == null)
                            {
                                _logger.LogInformation("Board not found.");
                            }

                            database.GetCollection<Model.Task>("tasks")
                                .DeleteMany(t => t.BoardId == deleted.Id);
                            database.GetCollection<Stage>("stages")
                                .DeleteMany(s => deleted.Stages.Contains(s.Id));

                            _logger.LogInformation("Board deleted successfully.");
                        }
                        catch (Exception e)
                        {

                        }

                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                };

                _channel.BasicConsume(queue: _queue, autoAck: false, consumer: consumer);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Dispose();
            }
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
