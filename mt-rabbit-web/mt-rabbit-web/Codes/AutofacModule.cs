using Autofac;
using MassTransit;

namespace mt_rabbit_web.Codes
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
                             ServiceBusFactory.New(sbc =>
                                                       {
                                                           sbc.UseRabbitMqRouting();
                                                           sbc.UseJsonSerializer();
                                                           sbc.ReceiveFrom("rabbitmq://localhost/web");
                                                       }))
                .As<IServiceBus>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}