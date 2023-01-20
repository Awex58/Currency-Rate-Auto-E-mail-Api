using WebApi.Core.Entities.Abstract;

namespace WebApi.Core.Entities.Dtos
{
    public class UserForLoginDto:IEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
