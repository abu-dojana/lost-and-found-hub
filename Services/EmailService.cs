using System.Net.Mail;
using System.Threading.Tasks;

namespace LAF.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendContactEmail(string fromEmail, string toEmail, string itemName)
        {
            // Example using SmtpClient (for development purposes only)
            using var client = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-password"),
                EnableSsl = true
            };

            var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = $"LAF Contact Regarding: {itemName}",
                Body = "A user wants to contact you about your post..."
            };

            await client.SendMailAsync(message);
        }
    }
}