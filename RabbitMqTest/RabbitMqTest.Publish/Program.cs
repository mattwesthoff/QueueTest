using System;
using RabbitMQ.Client;

namespace RabbitMqTest.Publish
{
    class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("logs", "fanout");

                string message = (args.Length > 0) ? string.Join(" ", args)
                                                   : "info: Hello World!";
                byte[] body = System.Text.Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("logs", "", null, body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
