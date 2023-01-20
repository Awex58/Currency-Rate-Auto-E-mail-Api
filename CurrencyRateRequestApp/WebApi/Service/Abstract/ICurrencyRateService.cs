using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Entities.Concrete;

namespace WebApi.Service.Abstract
{
    public interface ICurrencyRateService
    {
        CurrencyRate Add(string code);
        CurrencyRate GetBylastDate(string code);
    }
}
