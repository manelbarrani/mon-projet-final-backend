using RabbitMQ.Client;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Application.Services.BrokerService
{
    public class BrokerProducer : IBrokerProducer
    {
        public void SendMessage<T>(T message,string QueueName)
        {
            // var conn = _factory.CreateConnection();
            var factory = new ConnectionFactory();
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                factory = new ConnectionFactory
                {
                    HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
                    Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")),
                    UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER"),
                    VirtualHost = "/",
                    Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"),
                    DispatchConsumersAsync = true
                };
            else
                factory = new ConnectionFactory
                {
                    HostName = "rabbitmq-sub.dev2.addinn-group.com",
                    Port = 30903,
                    UserName = "user",
                    VirtualHost = "/",
                    Password = "HAd4xTb9fwsuLJ1l",
                    DispatchConsumersAsync = true
                };
            var conn = factory.CreateConnection();
            using var channel = conn.CreateModel();
            channel.QueueDeclare(QueueName, durable: true, exclusive: false,autoDelete:true);
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish("", QueueName, body:body);
            Console.WriteLine("test");
        }
    }
}
