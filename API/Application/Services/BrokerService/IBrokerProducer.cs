namespace Application.Services.BrokerService
{
    public interface IBrokerProducer
    {
        public void SendMessage<T>(T message,string QueueName);
    }
}
