using System;
using System.Threading.Tasks;
using Consumer.Consumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Notification.MailService;

namespace Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string queue = "Mail-queue"; // que olusturmuyor publisher

            ServiceProvider serviceProvider = new ServiceCollection().AddScoped<IMailService, MailManager>()
                    .AddMassTransit(x => x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(factory =>
                    {

                        factory.Host("amqps://localhost:5672", configurator =>
                        {
                            configurator.Username("guest");
                            configurator.Password("guest");
                        });

                        factory.UseCircuitBreaker(configurator =>
                        {
                            configurator.TrackingPeriod = TimeSpan.FromMinutes(1); //Hata durumlarından sonra ne kadar süre takipte kalınacağını ifade etmektedir.
                            configurator.TripThreshold = 15; //Alınan taleplerin yüzdelik olarak ne kadarının hatalı olacağını ifade etmektedir.
                            configurator.ActiveThreshold = 10; //Üst üste alınabilecek hata sayısını ifade etmektedir.
                            configurator.ResetInterval = TimeSpan.FromMinutes(5); //Hata alındığında ne kadar süre beklenmesi gerektiğini ifade etmektedir.
                        });

                        factory.ReceiveEndpoint(queue, x => x.Consumer<MessageConsumer>(provider));
                        factory.UseMessageRetry(r => r.Immediate(5)); //tarafımızca belirtilen bir miktar ve aralıkta yeniden mesajları işlemesini isteyebiliriz.
                        factory.UseRateLimit(1000, TimeSpan.FromMinutes(1)); //Belirli Bir Süre İçerisinde İşlenecek Mesaj Adedini Belirleme

                    }))).AddScoped<MessageConsumer>()
                    .BuildServiceProvider();


            IBusControl busControl = serviceProvider.GetService<IBusControl>();

            await busControl.StartAsync();
            Console.ReadLine();
            await busControl.StopAsync();
        }
    }
}

