using Microsoft.CodeAnalysis.CSharp.Syntax;
using RabbitMQ.Client;
using System.Text;

namespace ElearingEnglis.rabbitMQ;

public static class ServiceRabbit 
{
    private static ConnectionFactory factory = new ConnectionFactory()
    {
         HostName = "localhost",
         Port = 5672,
         UserName = "guest",
         Password = "guest"
    };

    
     private static IConnection Connection ;
     private static IChannel  channel ;
     
     private static  bool isConnected = false;

     private static async Task<bool> Connect()
     {
        if (!isConnected)
        {
            Connection = await factory.CreateConnectionAsync();
            channel = await Connection.CreateChannelAsync();
            isConnected = true;
        }
        return true;
     }


    public static async Task<bool> createQueue(string queueName)
    {   

        await Connect();
        try{
        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error creating queue: {ex.Message}");
            return false;
        }
    }

    public static async Task<bool> DeleteQueue(string queueName)
    {   

        throw new NotImplementedException();
    }

    public static async Task<string> ReceiveMessage(string queueName)
    {   
        throw new NotImplementedException();
    }

    public static async Task<bool> SendMessage(string message, string queueName)
    {   
        var body = Encoding.UTF8.GetBytes(message);
        try
        {
            await Connect();
            await channel.BasicPublishAsync( exchange: "",
                                            routingKey: queueName,
                                            body: body);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error connecting to RabbitMQ: {ex.Message}");
            return false;
        }
        
        return true;
    }
}
