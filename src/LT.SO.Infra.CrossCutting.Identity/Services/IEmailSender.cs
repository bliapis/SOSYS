using System.Threading.Tasks;

namespace LT.SO.Infra.CrossCutting.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        bool SendEmail(string email, string subject, string message);
        Task SendResetPasswordAsync(string email, string callBackUrl);
    }
}