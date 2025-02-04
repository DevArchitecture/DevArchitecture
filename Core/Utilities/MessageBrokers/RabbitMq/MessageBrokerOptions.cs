namespace Core.Utilities.MessageBrokers.RabbitMq
{
    public class MessageBrokerOptions
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }
}