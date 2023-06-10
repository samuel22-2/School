using System.Net.Mail;
using System.Net;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace WebApplication3.models
{
    public class Email
    {

        public static void SendEmail(string recieveremail, string recieverName, string subject, string body)
        {
            using (var client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("osahonemovon080@gmail.com", "lqhotqkkmohpcxth");
                using (var message = new MailMessage(
                    from: new MailAddress("osahonemovon080@gmail.com", "Cyber University"),
                    to: new MailAddress(recieveremail, recieverName)

                    ))
                {

                    message.Subject = subject;
                    message.Body = body;

                    client.Send(message);
                }
            }
        }
    }
}

    




