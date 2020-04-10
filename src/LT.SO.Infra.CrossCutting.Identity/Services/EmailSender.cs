using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System;

namespace LT.SO.Infra.CrossCutting.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;

        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            throw new System.NotImplementedException();
        }

        public bool SendEmail(string email, string subject, string message)
        {
            //var client = new SmtpClient(host, port)
            //{
            //    Credentials = new NetworkCredential(userName, password),
            //    EnableSsl = enableSSL
            //};

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Credentials = new NetworkCredential(userName, password);
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = enableSSL;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;// disable it
                /// Now specify the credentials 
                smtpClient.Credentials = new NetworkCredential(userName, password);

                try
                {
                    smtpClient.Send(new MailMessage(userName, email, subject, message) { IsBodyHtml = true });
                    return true;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return false;
                }
                
            }

            //return client.SendMailAsync(
            //    new MailMessage(userName, email, subject, message) { IsBodyHtml = true }
            //);
        }

        public Task SendResetPasswordAsync(string email, string callBackUrl)
        {
            return Task.CompletedTask;
        }
    }
}