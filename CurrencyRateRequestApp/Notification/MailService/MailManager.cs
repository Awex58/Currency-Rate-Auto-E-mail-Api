using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit;
using Notification.Entities.MailSettings;
using WebApi.Core.Entities.Abstract;
using CurrencyRate = WebApi.Core.Entities.Concrete.CurrencyRate;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Notification.MailService
{
    public class MailManager: IMailService
    {
       private readonly MailSettings _mailSettings = new MailSettings();
        public async Task SendCurrencyEmailAsync(IMailData request)
        {
            string FilePath = "C:\\Users\\sahabt\\source\\repos\\CurrencyRateRequestApp\\Notification\\Templates\\MailTemplate.html";
            StreamReader str = new(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            

            MailText = MailText.Replace("[username]", request.FirstName).Replace("[email]", request.Email);
            MailText = PutOnTable(MailText, request.currencies);
            


            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Email);
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.Subject = $"Daily Exchange Rates {request.FirstName}";
            email.From.Add(new MailboxAddress(_mailSettings.Name, _mailSettings.Email));


            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Username, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public string PutOnTable(string mailtext , List<CurrencyRate> currencys)
        {
            if (currencys != null)
            {
                for (int i = 0; i < currencys.Count; i++)
                {
                    mailtext = mailtext.Replace("[ep" + i+"]",
                        "[CurrencyCode"+i+"] <table style=\"width: 100%\" border=\"1\"><tr>\r\n<td>ForexBuying</td>\r\n<td>ForexSelling</td>\r\n<td>BanknoteBuying</td>\r\n<td>BanknoteSelling</td>\r\n</tr>\r\n<tr>\r\n<td>[forexbuyingvalue" + i+"]</td>\r\n<td>[forexsellingvalue"+i+"]</td>\r\n<td>[banknotebuyingvalue"+i+"]</td>\r\n<td>[banknotesellingvalue"+i+ "]</td>\r\n</tr> </table>");

                    mailtext= mailtext.Replace("[CurrencyCode"+i+"]", currencys[i].CurrencyCode)
                        .Replace("[forexbuyingvalue"+i+"]", Convert.ToString(currencys[i].ForexBuying)+ " TL")
                        .Replace("[forexsellingvalue"+i+"]", Convert.ToString(currencys[i].ForexSelling) + " TL")
                       .Replace("[banknotebuyingvalue"+i+"]", Convert.ToString(currencys[i].BanknoteBuying) + " TL")
                       .Replace("[banknotesellingvalue"+i+"]", Convert.ToString(currencys[i].BanknoteSelling) + " TL");
                }
                for (int i = 0; i <= 23; i++)
                {
                    mailtext = mailtext.Replace("[ep" + i + "]", "");
                }
            }
            return mailtext;
        }
        

    }


}
