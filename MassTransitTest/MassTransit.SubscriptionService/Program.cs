using System;
using Magnum.Extensions;
using MassTransit.Saga;
using MassTransit.Services.Subscriptions.Server;
using MassTransit.Transports.Stomp;
using MassTransitTest;


namespace MassTransit.SubscriptionService
{
    class Program
    {
        static void Main(string[] args)
        {
            StartSubscriptionService();
        }

        private static void StartSubscriptionService()
        {
            Console.Out.WriteLine("starting the subscription service");

            var subscriptionSagaRepository = new InMemorySagaRepository<SubscriptionSaga>();
            var clientSagaRepository = new InMemorySagaRepository<SubscriptionClientSaga>();


            using (var serviceBus =
                ServiceBusFactory.New(sbc =>
                {
                    sbc.UseStomp();

                    sbc.ReceiveFrom("{0}/mt_subscriptions".FormatWith(Constants.HostUri));
                    sbc.SetConcurrentConsumerLimit(1);
                }))
            {
                using (var subscriptionService = new Services.Subscriptions.Server.SubscriptionService(serviceBus, subscriptionSagaRepository,
                                                                  clientSagaRepository))
                {
                    subscriptionService.Start();
                    Console.WriteLine("ready... type 'exit' to stop");
                    while (Console.ReadLine() != "exit")
                    {
                        Console.WriteLine("type 'exit' to stop");
                    }
                }
            }
        }
    }
}
