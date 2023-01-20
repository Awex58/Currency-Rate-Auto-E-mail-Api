using System.Collections.Generic;
using WebApi.Core.Entities.Concrete;
using WebApi.Core.Entities.Dtos;
using WebApi.Data.Repository;

namespace WebApi.Core.DataAccess.EntityFrameworkDal.Abstract
{
    public interface IUserDal:IEntityRespository<User>
    {
        List<OperationClaim> GetClaims(User user);
        List<EmailAndUsernameDto> GetAllEmailsAndUsernames();
    }
}
