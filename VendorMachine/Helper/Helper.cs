using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace VendorMachine.Helper
{
    public class HelperClass
    {
        public static void SendEmail()
        {
            MailMessage mail = new MailMessage("alkesh.naik@naik.net", "alkesh.naik@bentley.com");
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("username", "password");
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "mailhost.bentley.com";
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);
        }
    }
}
