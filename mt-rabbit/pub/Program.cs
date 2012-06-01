using System;
using System.Threading;
using MassTransit;
using core;

namespace pub
{
    internal class Program
    {
        private static IServiceBus _bus;

        private static void Main(string[] args)
        {
            _bus = ServiceBusFactory.New(sbc =>
                                             {
                                                 sbc.UseRabbitMqRouting();
                                                 sbc.ReceiveFrom("rabbitmq://localhost/matt1");
                                                 sbc.UseJsonSerializer();
                                                 sbc.Subscribe(s => s.Handler<Response>(HandleMessage));
                                             });

            var id = 0;
            while (true)
            {
                _bus.Publish(new Request {CorrelationId = id, Text = "Hiiiii"});
                Console.Out.WriteLine("Published request " + id);
                id++;
                Thread.Sleep(5000);
            }
        }

        private static void HandleMessage(Response msg)
        {
            Console.Out.WriteLine(string.Format("got {1} response for msg {0}", msg.CorrelationId,
                                                msg.Successful ? "Successful" : "Failed"));
        }
    }
}