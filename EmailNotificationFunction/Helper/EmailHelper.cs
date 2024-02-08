using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotificationFunction.Helper
{
    internal class EmailHelper
    {
        private readonly ILogger _log;
        public EmailHelper(ILogger log)
        {
            _log = log;
        }
        internal async Task SendEmail(string recipientEmail, string subject, string body)
        {
            string smtpServer = Environment.GetEnvironmentVariable("SmtpServer");
            int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SmtpPort"));
            string smtpUsername = Environment.GetEnvironmentVariable("SmtpUsername");
            string smtpPassword = Environment.GetEnvironmentVariable("SmtpPassword");

            try { 
                
                if(string.IsNullOrEmpty(smtpServer) || smtpPort == 0 || string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
                {
                    _log.LogError("Please provide valid SMTP server, port, username, and password in the application settings.");
                    return;
                }

                if(smtpUsername == "test") // Added for demo purpose only
                {
                    _log.LogInformation("Sending Email now...");
                    return;
                }
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add(recipientEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(mailMessage);
                    _log.LogInformation($"Email sent to {recipientEmail} successfully");
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"Error sending email: {ex.Message}");
            }
        }
    }
}
