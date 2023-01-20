using WebApi.Core.DataAccess.EntityFrameworkDal.Abstract;
using WebApi.Core.Entities.Concrete;
using WebApi.Data.DbContext;
using WebApi.Data.Repository;

namespace WebApi.Core.DataAccess.EntityFrameworkDal.Concrete
{
    public class EfCurrenyRateDal : EfEntityRepositoryBase<CurrencyRate, DbCurrencyContext>, ICurrencyRateDal
    {
    }
}
