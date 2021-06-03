using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Setting.Service
{
    public interface ISmtpService
    {
        Task Send(SmtpClient client, MailMessage message);
    }

    public class SmtpService : ISmtpService
    {
        public async Task Send(SmtpClient client, MailMessage message)
        {
            await client.SendMailAsync(message);
        }
    }
}
