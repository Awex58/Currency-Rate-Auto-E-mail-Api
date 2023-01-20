using System;
using MassTransit;
using WebApi.Core.DataAccess.EntityFrameworkDal.Abstract;
using WebApi.Core.DataAccess.EntityFrameworkDal.Concrete;
using WebApi.Core.DataAccess.TcmbAccess.Abstract;
using WebApi.Core.Entities.Concrete;
using WebApi.Core.MassTransit.Configration;

namespace WebApi.Core.MassTransit.Producer

{
    public class Producer
    {
        MassTransitConfigration bussConfigration;
        public IBusControl bus;
        private ITcmbAccess _tcmbAccess;
        private IUserDal _userDal;
        public Producer(ITcmbAccess tcmbAccess, IUserDal userDal)
        {
            _tcmbAccess = tcmbAccess;
            _userDal = userDal;
        }

        public async void ProducerPublish()
        {
            if (bussConfigration == null)
            {
                bussConfigration = new MassTransitConfigration();
                bus = bussConfigration.bus();
            }

            var sendToUri = new Uri("amqps://localhost:5672/Mail-queue");
            //var endPoint = await bus.GetSendEndpoint(sendToUri);

            var endPoint = await bus.GetSendEndpoint(sendToUri);


            var allEmailsAndUsernames = _userDal.GetAllEmailsAndUsernames();
            

            foreach (var maildata in allEmailsAndUsernames)
            {
                await endPoint.Send(new MailData
                {
                    Email = maildata.Email,
                    FirstName = maildata.Firstname,
                    currencies = _tcmbAccess.AllTcmbData()
                });
               
            }

            


        }
    }

}