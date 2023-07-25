using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models.MailServices
{
    public abstract class MasterMailServices
    {
        private SmtpClient _smtpClient;
        protected string _senderEmail;
        protected string _passwordEmail;
        protected string _host;
        protected int _port;
        protected bool _ssl;

        protected void initializeSmtpClient()
        {

            _smtpClient = new SmtpClient();
            _smtpClient.Credentials = new NetworkCredential(_senderEmail, _passwordEmail);
            _smtpClient.Host = _host;
            _smtpClient.Port = _port;
            _smtpClient.EnableSsl = _ssl;
        }




        public async Task sendMail(string subject, string body, List<string> recipientMail)
        {
            var mailMessage = new MailMessage();

            try
            {
                mailMessage.From = new MailAddress(_senderEmail);
                foreach (var mail in recipientMail)
                {
                    mailMessage.To.Add(mail);

                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex) { }
            finally
            {
                mailMessage.Dispose();
                _smtpClient?.Dispose();
            }
        }
    }
}
