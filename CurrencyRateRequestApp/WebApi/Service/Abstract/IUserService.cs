using System.Collections.Generic;
using WebApi.Core.Entities.Concrete;

namespace WebApi.Service.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void add(User user);
        User GetByMail(string email);
    }
}
