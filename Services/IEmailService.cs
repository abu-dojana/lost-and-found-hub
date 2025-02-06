namespace LAF.Services
{
    public interface IEmailService
    {
        Task SendContactEmail(string fromEmail, string toEmail, string itemName);
    }
}