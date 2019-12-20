using System.Net.Mail;

namespace Sum.Service.Features
{
    public interface IEmailSender
    {
        bool SendEmail(string email, string subject, string message, Attachment[] attachments);
    }
}