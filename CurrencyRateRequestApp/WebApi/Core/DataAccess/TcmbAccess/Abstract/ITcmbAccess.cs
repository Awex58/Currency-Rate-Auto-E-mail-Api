using System.Collections.Generic;
using WebApi.Core.Entities.Concrete;

namespace WebApi.Core.DataAccess.TcmbAccess.Abstract
{
    public interface ITcmbAccess
    {
        public CurrencyRate TcmbAccessRequest(string code);
        public List<CurrencyRate> AllTcmbData();
    }
}
