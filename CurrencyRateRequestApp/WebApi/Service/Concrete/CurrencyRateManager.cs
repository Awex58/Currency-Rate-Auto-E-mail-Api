using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.Core.DataAccess.EntityFrameworkDal.Abstract;
using WebApi.Core.DataAccess.TcmbAccess.Abstract;
using WebApi.Core.Entities.Concrete;
using WebApi.Service.Abstract;
using WebApi.Service.Redis;

namespace WebApi.Service.Concrete
{
    public class CurrencyRateManager:ICurrencyRateService
    {
        private ICurrencyRateDal _currencyRateDal;
        private ITcmbAccess _tcmbAccess;
        private ICacheService _cacheService;
        private ICurrencyService _currencyService;
        private static List<CurrencyRate> currencyRatelist = new List<CurrencyRate>();

        public CurrencyRateManager(ICurrencyRateDal currencyRateDal, ITcmbAccess tcmbAccess, ICacheService cacheService, ICurrencyService currencyService)
        {
            _currencyRateDal = currencyRateDal;
            _tcmbAccess = tcmbAccess;
            _cacheService = cacheService;
            _currencyService = currencyService;
        }
        public CurrencyRate Add(string code)
        {

            var allCurrencyRates = _cacheService.Get<List<CurrencyRate>>("money");

            if(allCurrencyRates != null ){
                foreach (var currencyRate in
                         allCurrencyRates) // rediste var mı diye kontrol ediyor varsa direkt response ediyor.
                {
                    
                        if (currencyRate != null && code == currencyRate.CurrencyCode && currencyRate.CurrencyDate.ToString("dd-MM-yyyy") ==
                            DateTime.Now.Date.ToString("dd-MM-yyyy"))
                        {
                            return currencyRate;
                        }
                    
                    
                }
            }

            if (_currencyService.GetByCode(code) != null) //currency tablosun da kayıtlı mı diye kontrol ediyor.
            {
                _currencyRateDal.Add(_tcmbAccess.TcmbAccessRequest(code)); //  kur değerlerini tcmb'den çekip databaseye kayıt ediyor.

                var getbylastdate = GetBylastDate(code);

                currencyRatelist.Add(getbylastdate); // currentrate databasinden kuru çekiyor liste ekliyor.
                
                _cacheService.Add("money", currencyRatelist); // listi redise ekliyor.

                return getbylastdate; // code'a uyan ve son tarihli veriyi kullanıcıya yansıtıyor



            }
            else // currencyde yok ise
            {
                return default;
            }


        }

       public CurrencyRate GetBylastDate(string code) // code karşılığına gelen time datesi en küçüğü getiriyor
       {
           var aa = _currencyRateDal.GetList(x => x.CurrencyCode == code).OrderByDescending(x => x.CurrencyDate)
               .FirstOrDefault();
           

            return aa;
        }

    }
}

