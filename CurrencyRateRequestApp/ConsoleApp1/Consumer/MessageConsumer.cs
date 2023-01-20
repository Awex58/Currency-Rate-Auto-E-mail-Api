using System;
using MassTransit;
using System.Threading.Tasks;
using Notification.MailService;
using WebApi.Core.Entities.Abstract;

namespace Consumer.Consumer
{
    public class MessageConsumer : IConsumer<IMailData>
    {
        private readonly IMailService _mailService;
        public MessageConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task Consume(ConsumeContext<IMailData> context)
        {

            Console.WriteLine($"Email Gönderildi : Email: {context.Message.Email + " First Name: " + context.Message.FirstName}");
            await  _mailService.SendCurrencyEmailAsync(context.Message);

            await Task.CompletedTask;
        }


    }
}

