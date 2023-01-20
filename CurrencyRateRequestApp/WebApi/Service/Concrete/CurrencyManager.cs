using WebApi.Core.DataAccess.EntityFrameworkDal.Abstract;
using WebApi.Core.DataAccess.TcmbAccess.Concrete;
using WebApi.Core.Entities.Concrete;
using WebApi.Core.Entities.Result;
using WebApi.Service.Abstract;

namespace WebApi.Service.Concrete
{
    public class CurrencyManager:ICurrencyService
    {
        private ICurrencyDal _currencyDal;

        public CurrencyManager(ICurrencyDal currencyDal)
        {
            _currencyDal = currencyDal;
        }
        public CurrencyRate GetCurrency(string code)
        {
            TcmbAccess deneme = new TcmbAccess();
            return deneme.TcmbAccessRequest(code);

        }
        public IDataResult<Currency> GetByCode(string code)
        {
            return new SuccessDataResult<Currency>(_currencyDal.Get(p => p.Code == code));
        }

        public IResult Add(Currency currency)
        {
             _currencyDal.Add(currency);
             return new SuccessResult("Currency Added!");
        }

        public IResult Delete(Currency currency)
        {
            _currencyDal.Delete(currency);
            return new SuccessResult("Currency Deleted!");
        }

        public IResult Update(Currency currency)
        {
             _currencyDal.Update(currency);
             return new SuccessResult("Currency Updated!");
             

        }
    }
}
