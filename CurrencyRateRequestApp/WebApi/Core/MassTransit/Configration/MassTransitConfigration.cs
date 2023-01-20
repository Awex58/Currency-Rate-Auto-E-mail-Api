using MassTransit;

namespace WebApi.Core.MassTransit.Configration
{
    public class MassTransitConfigration
    {
        //string queue = "Mail-queue";
        public IBusControl bus()
        {
            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host("amqps://localhost:5672", configurator =>
                {
                    configurator.Username("guest");
                    configurator.Password("guest");
                });

            });
            return bus;

        }

    }
}
