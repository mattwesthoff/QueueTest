using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace RabbitMqTest.NewTask
{
    class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare("task_queue", true, false, false, null);
                for (var i = 1; i <= 10; i++ )
                {
                    var dots = new List<string>();
                    for (var j = 10; j > i; j-- )
                    {
                        dots.Add(".");
                    }
                    ScheduleTask(string.Format("Message {0}{1}", i, string.Join("", dots.ToArray())), channel);                    
                }
            }
        }

        private static void ScheduleTask(string message, IModel channel)
        {
            

            message = (!String.IsNullOrEmpty(message))
                                 ? message
                                 : "Hello World!";
            byte[] body = System.Text.Encoding.UTF8.GetBytes(message);

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish("", "task_queue", properties, body);
            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}
