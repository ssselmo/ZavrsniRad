using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DonorCentar.Helper
{
    public static class EmailHelper
    {
        public static void SendMail( string to, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("solidnasolida@gmail.com");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;


                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    //smtp.Credentials = new NetworkCredential("donorcentar@gmail.com", "rs1seminarski");
                    smtp.Credentials = new NetworkCredential("solidnasolida@gmail.com", "Solidna!Solida1");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

    }
}
