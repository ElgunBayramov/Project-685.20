﻿using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project.WebUI.AppCode.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions options;

        public EmailService(IOptions<EmailServiceOptions> options)
        {
            this.options = options.Value;
        }
        public async Task<bool> SendEmailAsync(string toEmail, string approveLink)
        {
            string fromEmail = options.UserName;
            SmtpClient smtpClient = new SmtpClient(options.SmtpHost, options.SmtpPort);
            smtpClient.Credentials = new NetworkCredential(fromEmail, options.Password);
            smtpClient.EnableSsl = true;

            MailAddress from = new MailAddress(fromEmail, options.DisplayName);
            MailAddress to = new MailAddress(toEmail);

            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = options.Subject;
            mailMessage.Body = "Məmnun olduq,<br/>Zəhmət olmasa abunəliyinizi  " +
                    $"<a href='{approveLink}'>link</a> vasitəsilə tamamlayasınız.";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string message)
        {
            string fromEmail = options.UserName;
            SmtpClient smtpClient = new SmtpClient(options.SmtpHost, options.SmtpPort);
            smtpClient.Credentials = new NetworkCredential(fromEmail, options.Password);
            smtpClient.EnableSsl = true;

            MailAddress from = new MailAddress(fromEmail, options.DisplayName);
            MailAddress to = new MailAddress(toEmail);

            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }

    }
    public class EmailServiceOptions
    {
        public string DisplayName { get; set; }
        public string SmtpHost { get; set; }
        public string Subject { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
