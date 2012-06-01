using System;
using MassTransit;
using core;

namespace sub
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
                        sbc.ReceiveFrom("rabbitmq://localhost/matt2");
                        sbc.UseJsonSerializer();
                        sbc.Subscribe(s => s.Handler<Request>(HandleMessage));
                    });
        }

        private static void HandleMessage(Request msg)
        {
            _bus.MessageContext<Request>().Respond(new Response
                                                       {
                                                           CorrelationId = msg.CorrelationId,
                                                           Successful = msg.CorrelationId%6 == 0
                                                       });
            Console.Out.WriteLine(String.Format("got request {0}! text: {1}", msg.CorrelationId,
                                                msg.Text));
        }
    }
}