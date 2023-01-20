using WebApi.Core.Entities.Concrete;
using WebApi.Core.Entities.Result;

namespace WebApi.Service.Abstract
{
    public interface ICurrencyService
    {
      

      IDataResult<Currency>  GetByCode(string code);
        IResult Add(Currency currency);
        IResult Delete(Currency currency);
        IResult Update(Currency currency);
        CurrencyRate GetCurrency(string code);
    }
}
