using Confluent.Kafka;
using Domain.DTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Services
{
    public class ReminderJobWorker : BackgroundService
    {
        private readonly ILogger<ReminderJobWorker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReminderJobWorker(ILogger<ReminderJobWorker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("NOTIFY is Started.");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            using var scope = _serviceScopeFactory.CreateScope();

            var _hub = scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();
            //var timer = new PeriodicTimer(TimeSpan.FromSeconds(15));
            //var a = new TrackingNotif()
            //{
            //    shipment_id = Guid.NewGuid(),
            //    temperature = 54.21,
            //    humidity = 80,
            //    luminosity = 54,
            //    choc_detector = false,
            //    magnetometer = 54,
            //    accelerometer = 54,
            //    gaz_quality = 15,
            //    gyro = 656,
            //    intrusion_control = 65,
            //    latitude = 36.514564,
            //    longitude = 10.192656
            //};
            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true
            //};
            //var jsonString = JsonSerializer.Serialize(a, options);
            //while (await timer.WaitForNextTickAsync(stoppingToken))
            //{
            //    await _hub.Clients.All.SendAsync("NotifTrack", jsonString);
            // }
            var config = KafkaConsumerConfig.GetConfig();
            string topic = "iot"; // Replace with your topic name
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(15));
            consumer.Subscribe(topic);
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume();
                        Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                        var obj = JsonSerializer.Deserialize<TrackingNotif>(consumeResult.Message.Value);
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true
                        };
                        var jsonString = JsonSerializer.Serialize<TrackingNotif>(obj, options);

                        await _hub.Clients.All.SendAsync("NotifTrack", jsonString);

                        // Process the message here
                        consumer.Commit(consumeResult);
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error consuming message: {e.Error.Reason}");
                    }
                }
            }
         


        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("NOTIFY is closed.");
        }
    }
}
