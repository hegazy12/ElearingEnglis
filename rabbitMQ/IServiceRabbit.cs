namespace ElearingEnglis.rabbitMQ;

public interface IServiceRabbit
{
   public bool SendMessage (string message, string queueName);
   public string ReceiveMessage(string queueName);
   public bool createQueue(string queueName);
   public bool DeleteQueue(string queueName);
}
