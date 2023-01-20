using WebApi.Core.Entities.Concrete;
using WebApi.Core.Entities.Dtos;
using WebApi.Core.Entities.Result;
using WebApi.Core.Utilites.Hashing;
using WebApi.Core.Utilites.Security.Jwt;
using WebApi.Service.Abstract;

namespace Business.Concrete
{
    public class AuthManager:IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        { 
            byte[] passwordHash, passwordSalt;
           HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
           var user = new User
           {
               Email = userForRegisterDto.Email,
               FirstName = userForRegisterDto.FirstName,
               LastName = userForRegisterDto.LastName,
               PasswordHash = passwordHash,
               PasswordSalt = passwordSalt,
               Status = true
           };
           _userService.add(user);
           return new SuccessDataResult<User>(user, "User Registered!");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("User not found"); // messages classı kullanılabılır
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Password not found");
            }

            return new SuccessDataResult<User>(userToCheck, "Login success");
        }

        public IResult UserExits(string email)
        {
            if (_userService.GetByMail(email)!=null)
            {
                return new ErrorResult("Email already used!");
            }

            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken,"Access Token successful");

        }
    }
}
