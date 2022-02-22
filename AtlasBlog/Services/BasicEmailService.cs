using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace AtlasBlog.Services
{
    public class BasicEmailService : IEmailSender
    {
        private readonly IConfiguration _appSettings;

        public BasicEmailService(IConfiguration appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // -----   WE NEED TO COMPOSE AN EMAIL BASED PARTIALLY ON THE DATA SUPPLIED BY THE USER ----
            var emailSender = _appSettings["SmtpSettings:UserName"];

            MimeMessage newEmail = new();

            newEmail.Sender = MailboxAddress.Parse(_appSettings["SmtpSettings:UserName"]);
            newEmail.To.Add(MailboxAddress.Parse(email));
            newEmail.Subject = subject;

            // ---- BODY OF THE EMAIL ----- tricky ----<
            BodyBuilder emailBody = new();
            emailBody.HtmlBody = htmlMessage;
            newEmail.Body = emailBody.ToMessageBody();

            // ------  THIS POINT WE'RE DONE COMPOSING THE EMAIL AND NOW --------<
            // ------ OUR FOCUS IS ON CONFIGURING THE Simple Mail Trasfer Protocol (SMTP) server ----<
            using SmtpClient smtpClient = new();

            var host = _appSettings["SmtpSettings:Host"];
            var port = Convert.ToInt32(_appSettings["SmtpSettings:Port"]);

            smtpClient.Connect(host, port, SecureSocketOptions.StartTls);
            await smtpClient.AuthenticateAsync(emailSender, _appSettings["SmtpSettings:Password"]);
            await smtpClient.SendAsync(newEmail);
            await smtpClient.DisconnectAsync(true);

        }
    }
}
