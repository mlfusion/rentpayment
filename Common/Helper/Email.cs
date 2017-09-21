using System;
using System.Net;
using System.Net.Mail;

namespace Rent.Common.Helper
{
    public class Email
    {
        public static int MlfusionSmtp(MailAddress toAddress, string subject, string messageBody, bool html)
        {
            var count = 0;
            var r = 0;

            while (count <= 0)
            {
                if (r == 5)
                    break;

                count = Smtp(toAddress, subject, messageBody, html);

                r++;
            }
            return count;
        }

        private static int Smtp(MailAddress toAddress, string subject, string messageBody, bool html)
        {
            try
            {
                var fromAddress = new MailAddress(Rent.Common.Properties.Settings.Default.Email, "Rent Payment");
                var fromPassword = Properties.Settings.Default.Password;
                var smtp = new SmtpClient
                {
                    Host = Rent.Common.Properties.Settings.Default.MailHost,
                    EnableSsl = false,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Rent.Common.Properties.Settings.Default.Email, fromPassword)
                };

                using (MailMessage message = new MailMessage(fromAddress, toAddress)
                {Subject = subject, Body = messageBody, IsBodyHtml = html})
                {
                    smtp.Send(message);
                }
                return 1;
            }
            catch (Exception exception)
            {
                return 0;
            }
        }
    }
}
