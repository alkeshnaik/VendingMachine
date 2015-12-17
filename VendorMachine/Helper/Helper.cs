using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace VendorMachine.Helper
{
    public class HelperClass
    {
        static string ToEmail = ConfigurationManager.AppSettings["ToEmail"].ToString();
        static string FromName = ConfigurationManager.AppSettings["FromName"].ToString();
        static string SMTPGrid = ConfigurationManager.AppSettings["SMTPGrid"].ToString();
        static string port = ConfigurationManager.AppSettings["port"].ToString();

        public static void SendEmail(string Fromemail,string toName,string item,string cardnumber)
        {

            try
            {
                MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(ToEmail, toName));

                // From
                mailMsg.From = new MailAddress(Fromemail, FromName);

                // Subject and multipart/alternative Body
                mailMsg.Subject = @toName + " has requested for the item " + item;
                string text = "text body";
                string html = @"<p>"+toName+" has requested for the item " +item+"</p>";
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient(SMTPGrid, Convert.ToInt32(port));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("bentleysendgrid", "Bentley_123");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //MailMessage mail = new MailMessage("alkesh.naik@naik.net", "alkesh.naik@bentley.com");
            //SmtpClient client = new SmtpClient();
            //client.Credentials = new NetworkCredential("username", "password");
            //client.Port = 25;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Host = "mailhost.bentley.com";
            //mail.Subject = "this is a test email.";
            //mail.Body = "this is my test email body";
            //client.Send(mail);
        }
    }
}
