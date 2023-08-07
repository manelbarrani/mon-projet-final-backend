using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace Application.Services.BrokerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ConnectionFactory _connectionFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "QueueName";

        public Worker(ILogger<Worker> logger,IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                _connectionFactory = new ConnectionFactory
                {
                    HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
                    Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")),
                    UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER"),
                    VirtualHost = "/",
                    Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"),
                    DispatchConsumersAsync = true
                };
            else
                _connectionFactory = new ConnectionFactory
                {
                    HostName = "rabbitmq-sub.dev2.addinn-group.com",
                    Port = 30903,
                    UserName = "user",
                    VirtualHost = "/",
                    Password = "HAd4xTb9fwsuLJ1l",
                    DispatchConsumersAsync = true
                };
            try
            {
                _connection = _connectionFactory.CreateConnection();

            }
            catch
            {
                return base.StopAsync(cancellationToken);
            }
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: true);
            _logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (bc, ea) =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                   
                try
                {
                    await Task.Delay(new Random().Next(1, 3) * 1000, stoppingToken);
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (JsonException)
                {
                    _logger.LogError($"JSON Parse Error: ''.");
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                }
                catch (AlreadyClosedException)
                {
                    _logger.LogInformation("RabbitMQ is closed!");
                }
                catch (Exception e)
                {
                    _logger.LogError(default, e, e.Message);
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }
    }
}