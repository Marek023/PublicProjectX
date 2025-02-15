namespace ProjectX.Services.EmailService.Interface;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string mailTo, string subject, string body);
}