using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using ProjectX.Models.Options;
using ProjectX.Services.EmailService.Interface;

namespace ProjectX.Services.EmailService.Implementation;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly EmailOptions _emailOptions;

    public EmailService(
        ILogger<EmailService> logger,
        IOptions<EmailOptions> emailOptions)
    {
        _logger = logger;
        _emailOptions = emailOptions.Value;
    }

    public async Task<bool> SendEmailAsync(string mailTo, string subject, string body)
    {
        try
        {
            if (string.IsNullOrEmpty(mailTo) && string.IsNullOrEmpty(subject) && string.IsNullOrEmpty(body))
            {
                return false;
            }
            
            using (var client = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.SmtpPort))  
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_emailOptions.SenderEmail, _emailOptions.SenderPassword);
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                var mail = new MailMessage
                {
                    From = new MailAddress(_emailOptions.SenderEmail,"Změny v seznamech aktiv na globálních indexech"),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = body
                };
            
                mail.To.Add(mailTo);

                try 
                {
                    await client.SendMailAsync(mail);
                    _logger.LogInformation($"Email successfully sent to {mailTo}");
                }
                catch (SmtpException smtpEx)
                {
                    _logger.LogError(smtpEx, $"SMTP error while sending email to {mailTo}. " +
                                             $"Status code: {smtpEx.StatusCode}, Message: {smtpEx.Message}");
                    throw;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendEmailAsync");
            throw new Exception("Error in SendEmailAsync");
        }
    }
}