using System.Collections.Generic;
using WebApi.Core.Entities.Abstract;
using WebApi.Core.Entities.Concrete;

namespace Notification.Entities.MailEntities
{
   
    public class EmailData:IMailData
    {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public List<CurrencyRate> currencies { get; set; }
    }

}
