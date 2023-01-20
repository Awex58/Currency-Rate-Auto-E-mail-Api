using System.Collections.Generic;
using WebApi.Core.Entities.Concrete;

namespace WebApi.Core.Entities.Abstract
{
    public interface IMailData
    {
         string Email { get; set; }
         string FirstName { get; set; } 
         List<CurrencyRate> currencies { get; set; }
    }
}
