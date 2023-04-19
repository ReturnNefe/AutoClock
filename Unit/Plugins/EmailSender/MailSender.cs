using System.Net.Mail;

namespace EmailSender
{
    internal class MailSender
    {
        static public void Send(string mailServer, string sendKey, string sendUser, string receiveUser, string title, string content, string[] attachment, bool isHtmlBody = true)
        {
            var mail = new MailMessage();

            mail.From = new MailAddress(sendUser);
            mail.To.Add(receiveUser);

            mail.Subject = title;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = content;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = isHtmlBody;
            mail.Priority = MailPriority.Normal;

            if (attachment != null)
            {
                foreach (var iter in attachment)
                {
                    Attachment attachMent = new Attachment(iter);
                    mail.Attachments.Add(attachMent);
                }
            }

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(sendUser, sendKey);
            client.Host = mailServer;

            client.Send(mail);
        }
    }
}