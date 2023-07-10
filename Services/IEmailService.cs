using System.Threading.Tasks;

namespace app.Services
{
    public interface IEmailService
    {
        Task SendMail(string email, string username, string password);
        string CreateEmailBody(string userName, string Password);
    }
}