using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using WebApi.Core.DataAccess.EntityFrameworkDal.Abstract;
using WebApi.Core.Entities.Concrete;
using WebApi.Core.Entities.Dtos;
using WebApi.Data.DbContext;
using WebApi.Data.Repository;

namespace WebApi.Core.DataAccess.EntityFrameworkDal.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, DbCurrencyContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new DbCurrencyContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals
                        userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.UserId
                    select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }


        }
        public List<EmailAndUsernameDto> GetAllEmailsAndUsernames()
        {
            using (var context = new DbCurrencyContext())
            {
               var res = context.Users.Select(x => new EmailAndUsernameDto{
                   Email = x.Email,
                   Firstname = x.FirstName
               }).ToList();

                return res;
            }
        }
    }
}
