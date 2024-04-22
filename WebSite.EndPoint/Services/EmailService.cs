using System.Net.Mail;
using System.Net;
using System.Text;

namespace WebSite.EndPoint.Services
{
    public class EmailService
    {
        public Task Execute(string UserEmail, string Body, string Subject)
        {
            //enable less secure apps in account google with link
            //https://myaccount.google.com/lesssecureapps
            //hamideh.naserii00 @gmail.com
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 1000000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            //در خط بعدی ایمیل  خود و پسورد ایمیل خود  را جایگزین کنید
            client.Credentials = new NetworkCredential("bugeto.net@gmail.com", "password-khodet-ro-inja-bezar");
            MailMessage message = new MailMessage("bugeto.net@gmail.com", UserEmail, Subject, Body);
            message.IsBodyHtml = true;
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            client.Send(message);
            return Task.CompletedTask;
        }
    }
}
