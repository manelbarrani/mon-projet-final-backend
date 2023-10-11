using Application.Helper;
using Confluent.Kafka;
using System;
namespace Application.Services
{
    public static class KafkaConsumerConfig
    {
        public static ConsumerConfig GetConfig()
        {
            return new ConsumerConfig
            {
                BootstrapServers = EnvironmentVariablesHelper.GetUrlKafka(), // Replace with your Kafka broker address
                GroupId = EnvironmentVariablesHelper.GetGroupIDKafka(),
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = false,
            };
        }
    }
}
