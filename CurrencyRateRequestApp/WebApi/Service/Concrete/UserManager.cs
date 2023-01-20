using System.Collections.Generic;
using WebApi.Core.DataAccess.EntityFrameworkDal.Abstract;
using WebApi.Core.Entities.Concrete;
using WebApi.Service.Abstract;

namespace WebApi.Service.Concrete
{
    public class UserManager:IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void add(User user)
        {
           _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get((u => u.Email == email));
        }
    }
}
