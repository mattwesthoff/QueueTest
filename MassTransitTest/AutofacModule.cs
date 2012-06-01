using Autofac;
using MassTransit;
using MassTransit.Transports.Stomp;

namespace MassTransitTest
{
    public class AutofacModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => ServiceBusFactory.New(sbc =>
                                                            {
                                                                sbc.UseStomp();
                                                                sbc.UseSubscriptionService(Constants.SubscriptionServiceUri);
                                                                sbc.ReceiveFrom(Constants.ReceiveFromUri);
                                                                sbc.UseControlBus();
                                                                sbc.Subscribe(x => x.LoadFrom(c.Resolve<ILifetimeScope>()));

                                                            })).As<IServiceBus>().SingleInstance();
        }

    }
}
