using AssignmentManagement.IRepository;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using AssignmentManagement.Models;

namespace AssignmentManagement.Repository
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSettings;

        public EmailService(IOptions<EmailSetting> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendVerificationEmail(string email, string name, string verificationLink)
        {
            using var message = new MailMessage();

            message.From = new MailAddress(
                _emailSettings.SenderEmail,
                _emailSettings.SenderName
            );

            message.To.Add(email);

            message.Subject =
                "Verify Your Assignment Management Account";

            message.IsBodyHtml = true;

            message.Body = $@"
                <html>
                <body>

                    <h2>Welcome to Assignment Management</h2>

                    <p>
                        Hello {WebUtility.HtmlEncode(name)},
                    </p>

                    <p>
                        Thank you for registering.
                        Please verify your email address
                        by clicking the button below.
                    </p>

                    <p>
                        <a href=""{verificationLink}""
                           style=""
                           display:inline-block;
                           padding:10px 20px;
                           background:#007bff;
                           color:white;
                           text-decoration:none;
                           border-radius:5px;
                           "">
                            Verify My Email
                        </a>
                    </p>

                    <p>
                        This verification link will expire
                        in 30 minutes.
                    </p>

                    <p>
                        If you did not create this account,
                        please ignore this email.
                    </p>

                </body>
                </html>
            ";

            using var smtpClient = new SmtpClient(
                _emailSettings.SmtpServer,
                _emailSettings.Port
            );

            smtpClient.Credentials =
                new NetworkCredential(
                    _emailSettings.Username,
                    _emailSettings.Password
                );

            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(message);
        }
    }

}
