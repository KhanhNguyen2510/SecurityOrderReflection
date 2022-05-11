using MimeKit;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using SOR.ViewModel.Catalogs.SendEmail;
using SOR.Data.EFs;
using Microsoft.EntityFrameworkCore;
using SOR.Application.Catalogs.Users;

namespace SOR.Application.Catalogs.SendEmail
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly SORDbContext _context;
        private readonly IUserSevice _userSevice;
        public MailService(IOptions<MailSettings> mailSettings, SORDbContext context, IUserSevice userSevice)
        {
            _mailSettings = mailSettings.Value;
            _context = context;
            _userSevice = userSevice;
        }

        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            var gUser = await _context.Users.FirstOrDefaultAsync(x => x.IsDelete == true && x.UserName == request.userName);
            if (gUser == null) return;
            var gPassword = await _userSevice.UpdatePassWordToUser(request.userName);
            var password = gPassword.Result.ToString();

            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\SendEmailWelcome.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", gUser.UserName).Replace("[email]", gUser.Email).Replace("[password]", password);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(gUser.Email));
            email.Subject = $"Xin Chào {gUser.UserName}, chúng tôi từ SOR gửi cho bạn mã để thay đổi mật khẩu";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
