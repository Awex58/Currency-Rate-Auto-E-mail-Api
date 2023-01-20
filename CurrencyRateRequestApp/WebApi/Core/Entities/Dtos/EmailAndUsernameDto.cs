using WebApi.Core.Entities.Abstract;

namespace WebApi.Core.Entities.Dtos
{
    public class EmailAndUsernameDto : IDto {
        public string Email { get; set; }
        public string Firstname { get; set; }
    }
}
