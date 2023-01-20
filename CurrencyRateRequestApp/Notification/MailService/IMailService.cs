using System.Threading.Tasks;
using WebApi.Core.Entities.Abstract;

namespace Notification.MailService
{
    public interface IMailService
    {
     public Task SendCurrencyEmailAsync(IMailData request);
    }
}
