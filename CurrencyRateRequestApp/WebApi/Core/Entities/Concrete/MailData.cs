using System.Collections.Generic;
using WebApi.Core.Entities.Abstract;

namespace WebApi.Core.Entities.Concrete
{
    public class MailData : IMailData
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public List<CurrencyRate> currencies { get; set; }
    }
}
