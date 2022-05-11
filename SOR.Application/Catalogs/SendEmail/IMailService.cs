using SOR.ViewModel.Catalogs.SendEmail;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.SendEmail
{
    public interface IMailService
    {
        /// <summary>
        /// Send
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeRequest request);
    }
}
