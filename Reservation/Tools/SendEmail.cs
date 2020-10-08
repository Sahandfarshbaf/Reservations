using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace Reservation.Tools
{
    public class SendEmail
    {

        public void SendSuccessfullBuy(string fullName, string ticketNo, string emaill)
        {

            // create email message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("event@negahglobal.com");
            email.To.Add(MailboxAddress.Parse(emaill));

            email.Subject = "خرید بلیط";
            var body = fullName + " عزیز";
            body += System.Environment.NewLine;
            body += "خرید بلیط همایش شوالیه مدیران برتر با موفقیت انجام پذیرفت";
            body += System.Environment.NewLine;
            body += "شماره بلیط:  ";
            
            body += ticketNo;
           
            email.Body = new TextPart(TextFormat.Text) { Text = body };
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // hotmail
                //smtp.Connect("smtp.live.com", 587, SecureSocketOptions.StartTls);

                // office 365
                //smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("sahand.farshbaf@gmail.com", "sa19881213");
                smtp.Send(email);
                smtp.Disconnect(true);


            }
            catch (Exception ex)
            {


            }
            // send email

        }



    }
}
