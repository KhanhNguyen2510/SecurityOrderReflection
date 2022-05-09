using SOR.ViewModel.Catalogs.SendEmail;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.SendEmail
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeRequest request);
    }
}
