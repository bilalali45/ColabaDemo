using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using RainMaker.Common.FTP;
using RainMaker.Entity.Models;
using RainMaker.Common.Extensions;
using System.Threading.Tasks;

namespace RainMaker.Common.Email
{
    public class EmailSender
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSsl { get; set; }
        public List<Exception> SmtpException { get; set; }
        public NetworkCredential NetworkCredential { get; set; }

        public EmailSender(string host, int port, bool isSsl, NetworkCredential networkCredential)
        {
            Host = host;
            Port = port;
            IsSsl = isSsl;
            NetworkCredential = networkCredential;
            SmtpException = new List<Exception>();
        }

        public async Task<bool> SendEmailAsync(MailAddress fromAddress, EmailAccount fromAccount, MailAddress[] toAddress, MailAddress[] cc, MailAddress[] bcc, string testToEmailAddress, string subject, string body, FileProperties[] attachmentspath,
    bool isTestMode, string defaultToEmailAddress, string defaultFromDisplayName, int countReTries = 0, string emailKey = "")
        {
            Exception ex = null;
            using (SmtpClient smtp = new SmtpClient())
            {
                using (MailMessage message = new MailMessage())
                {
                    //System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                    //AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
                    AlternateView alternate = AlternateView.CreateAlternateViewFromString(body,System.Text.Encoding.UTF8 , "text/html");
                    alternate.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
                    message.AlternateViews.Add(alternate);

                    message.BodyEncoding = System.Text.Encoding.ASCII;
                    message.SubjectEncoding = System.Text.Encoding.ASCII;
                    message.BodyTransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
                    //message.IsBodyHtml = true;
                    message.Subject = subject;
                    message.Body = HtmlUtilities.ConvertToPlainText(body).Trim(); // set as plain text in email
                    message.From = fromAddress;
                    if (!string.IsNullOrWhiteSpace(emailKey))
                        message.Headers.Add("X-EmailLogId", emailKey);

                    if (fromAccount.UseReplyTo)
                        message.ReplyToList.Add(new MailAddress(fromAccount.Email, fromAccount.DisplayName));

                    if (isTestMode)
                    {
                        message.From = new MailAddress(defaultToEmailAddress, defaultFromDisplayName);
                        message.To.Add(testToEmailAddress);

                        //hasAddressToSend = true;
                        var bodyAdd = string.Empty;

                        if (fromAddress != null)
                            bodyAdd += string.Format("<div>From:'{0}' <br/>",
                                string.Format("'{0} {1}'", fromAddress.DisplayName ?? string.Empty, fromAddress.Address));

                        if (toAddress != null)
                            bodyAdd += string.Format("<div>To:'{0}' <br/>",
                                string.Join(",",
                                    toAddress.Select(
                                        s => string.Format("'{0} {1}'", s.DisplayName ?? string.Empty, s.Address))));
                        if (cc != null)
                            bodyAdd += string.Format("<div>CC:'{0}' <br/>",
                                string.Join(",",
                                    cc.Select(s => string.Format("'{0} {1}'", s.DisplayName ?? string.Empty, s.Address))));
                        if (bcc != null)
                            bodyAdd += string.Format("<div>BCC:'{0}' <br/>",
                                string.Join(",",
                                    bcc.Select(s => string.Format("'{0} {1}'", s.DisplayName ?? string.Empty, s.Address))));
                        message.Body = bodyAdd + message.Body;

                    }
                    else
                    {
                        if (toAddress != null)
                            foreach (var mailAddress in toAddress)
                            {
                                message.To.Add(mailAddress);
                            }

                        if (cc != null)
                            foreach (var mailAddress in cc)
                            {
                                message.CC.Add(mailAddress);
                            }

                        if (bcc != null)
                            foreach (var mailAddress in bcc)
                            {
                                message.Bcc.Add(mailAddress);
                            }
                    }

                    if (attachmentspath != null && attachmentspath.Any())
                    {

                        foreach (var attachment in attachmentspath)
                        {
                            var fs = new FileStream(attachment.FilePath, FileMode.Open, FileAccess.Read);
                            var attachData = new Attachment(fs, attachment.DisplayName);
                            message.Attachments.Add(attachData);
                        }
                    }

                    smtp.Host = this.Host;
                    smtp.Port = this.Port;
                    smtp.EnableSsl = this.IsSsl;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = this.NetworkCredential;
                    
                    try
                    {
                        await smtp.SendMailAsync(message);
                        return true;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                    }

                    finally
                    {
                        if (attachmentspath != null && attachmentspath.Any())
                            DisposeAttachments(message);
                        smtp.Dispose();
                    }
                }
            }

            if (countReTries >= Constants.ReTries)
            {
                SmtpException.Add(ex);
                GC.Collect();
                return false;
            }
            else
            {
                countReTries++;
                SmtpException.Add(ex);
                return await SendEmailAsync(fromAddress, fromAccount, toAddress, cc, bcc, testToEmailAddress, subject, body, attachmentspath, isTestMode, defaultToEmailAddress, defaultFromDisplayName, countReTries, emailKey);
            }
        }

        private void DisposeAttachments(MailMessage message)
        {
            foreach (Attachment attachment in message.Attachments)
            {
                attachment.Dispose();
            }
            message.Attachments.Dispose();
        }

        private async Task<bool> ResendOnExceptionAsync(int countReTries, Exception ex, MailAddress fromAddress, EmailAccount fromAccount, MailAddress[] toAddress, MailAddress[] cc, MailAddress[] bcc, string testToEmailAddress, string subject, string body, FileProperties[] attachmentspath,
            bool isTestMode, string defaultToEmailAddress, string defaultFromDisplayName)
        {
            if (countReTries >= Constants.ReTries)
            {
                SmtpException.Add(ex);
                return false;
            }
            else
            {
                countReTries++;
                SmtpException.Add(ex);
                return await SendEmailAsync(fromAddress, fromAccount, toAddress, cc, bcc, testToEmailAddress, subject, body, attachmentspath, isTestMode, defaultToEmailAddress, defaultFromDisplayName, countReTries);
            }
        }

    }
}