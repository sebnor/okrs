using System.Threading.Tasks;

namespace OKRs.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
