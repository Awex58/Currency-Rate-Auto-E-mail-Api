using System.Collections.Generic;
using WebApi.Core.Entities.Concrete;

namespace WebApi.Core.Utilites.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
