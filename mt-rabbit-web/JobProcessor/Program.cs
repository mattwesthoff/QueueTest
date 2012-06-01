using System;
using System.Threading;
using Core;
using MassTransit;

namespace JobProcessor
{
    internal class Program
    {
        private static IServiceBus _bus;

        private static void Main(string[] args)
        {
            _bus = ServiceBusFactory.New(
                sbc =>
                    {
                        sbc.UseRabbitMqRouting();
                        sbc.ReceiveFrom("rabbitmq://localhost/applayer");
                        sbc.UseJsonSerializer();
                        sbc.Subscribe(s => s.Handler<IStartJob>(HandleMessage));
                    });
        }

        private static void HandleMessage(IStartJob msg)
        {
          
            Console.Out.WriteLine(String.Format("got request {0}!", msg.CorrelationId));
            Thread.Sleep(10000);
            Console.Out.WriteLine("done with message" + msg.CorrelationId);
        }
    }
}