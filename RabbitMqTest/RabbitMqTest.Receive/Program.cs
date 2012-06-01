using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqTest.Receive
{
    class Program
    {
        public static void Main()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare("hello", true, false, false, null);

                var consumer = new QueueingBasicConsumer(channel);

                channel.BasicConsume("hello", true, consumer);

                System.Console.WriteLine(" [*] Waiting for messages." +
                                         "To exit press CTRL+C");
                while (true)
                {
                    var ea =
                        (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                    byte[] body = ea.Body;
                    string message = System.Text.Encoding.UTF8.GetString(body);
                    System.Console.WriteLine(" [x] Received {0}", message);
                }
            }
        }
    }
}
