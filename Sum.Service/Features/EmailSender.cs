using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using Sum.Model.Feature;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Sum.Service.Features
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostingEnvironment _env;
        public EmailSender(IOptions<EmailSettings> emailSettings, IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public bool SendEmail(string email, string subject, string message, Attachment[] attachments)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(email));
            mimeMessage.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = message;

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            return SmtpClient(mimeMessage);
        }

        public bool SmtpClient(MimeMessage mimeMessage)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                if (_env.IsDevelopment())
                {
                    client.Connect(_emailSettings.MailServer, _emailSettings.MailPort, false);
                }
                else client.Connect(_emailSettings.MailServer);

                client.Authenticate(_emailSettings.SenderEmail, _emailSettings.Password);
                client.Send(mimeMessage);
                client.Disconnect(true);
                return true;
            }
        }
    }
}