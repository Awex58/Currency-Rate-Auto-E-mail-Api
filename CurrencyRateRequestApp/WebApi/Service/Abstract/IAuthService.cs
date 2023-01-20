using WebApi.Core.Entities.Concrete;
using WebApi.Core.Entities.Dtos;
using WebApi.Core.Entities.Result;
using WebApi.Core.Utilites.Security.Jwt;

namespace WebApi.Service.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExits(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
