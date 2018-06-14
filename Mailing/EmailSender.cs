using ERISCOTools.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ERISCOTools.Mailing
{
    public class EmailSender
    {
        private string SITE_TITLE;
        private string EMAIL_FROM;
        private string EMAIL_NAME;
        private string DOMAIN;
        private string SMTP_HOST;
        private string SMTP_USER;
        private string SMTP_PASSWORD;
        private string LOGO_URL;

        public EmailSender(string SITE_TITLE, string EMAIL_FROM, string EMAIL_NAME, string DOMAIN, string SMTP_HOST, string SMTP_USER, string SMTP_PASSWORD, string LOGO_URL)
        {
            this.SITE_TITLE = SITE_TITLE;
            this.EMAIL_FROM = EMAIL_FROM;
            this.EMAIL_NAME = EMAIL_NAME;
            this.DOMAIN = DOMAIN;
            this.SMTP_HOST = SMTP_HOST;
            this.SMTP_USER = SMTP_USER;
            this.SMTP_PASSWORD = SMTP_PASSWORD;
            this.LOGO_URL = LOGO_URL;
        }

        public void Send(Email email)
        {
            try
            {
                EmailTemplate template = new EmailTemplate(SITE_TITLE,DOMAIN, LOGO_URL);
                email.message = template.HTML_BEGIN.ToString() + template.LOGO.ToString() + email.message.ToString() + template.HTML_END.ToString();

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                MailAddress mailAddress = new MailAddress(EMAIL_FROM, EMAIL_NAME);

                mail.To.Add(email.receiver);
                mail.From = mailAddress;
                mail.Subject = email.subject;
                mail.Body = email.message;

                if (!String.IsNullOrWhiteSpace(email.copyTo))
                {
                    mail.CC.Add(email.copyTo);
                }

                if (!String.IsNullOrWhiteSpace(email.replyTo))
                {
                    mail.ReplyToList.Add(email.replyTo);
                }

                if (!String.IsNullOrWhiteSpace(email.attachment_path))
                {
                    email.attachment_path = DOMAIN + email.attachment_path;
                    var stream = new WebClient().OpenRead(email.attachment_path);
                    Attachment data = new Attachment(stream, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = DateTime.Now;
                    disposition.ModificationDate = DateTime.Now;
                    disposition.ReadDate = DateTime.Now;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    disposition.FileName = email.attachment_name;
                    mail.Attachments.Add(data);
                }

                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtp.Host = SMTP_HOST;

                NetworkCredential basicCredential = new NetworkCredential(SMTP_USER, SMTP_PASSWORD);
                smtp.Credentials = basicCredential;

                smtp.Send(mail);

            }
            catch (SmtpException smtpEx)
            {
                throw smtpEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
