using System;
using RabbitMQ.Client;

namespace RabbitMqTest.Send
{
    class Program
    {

        public static void Main()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("hello", false, false, false, null);

                var message = "Hello World!";
                byte[] body = System.Text.Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", "hello", null, body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

    }
}
