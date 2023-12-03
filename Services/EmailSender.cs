using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication5.Services
{
    public class EmailSender : IEmailSender
    {
       
            public Task SendEmailAsync(string email, string subject, string message)
            {

                var mail = "shoppingstore1234567@gmail.com";
                var pw = "sh0pping@1";
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    //UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mail, pw)
                };

                return client.SendMailAsync(
                    new MailMessage(from: mail,
                                    to: email,
                                    subject,
                                    message
                                    ));
            }
        
    }
}
