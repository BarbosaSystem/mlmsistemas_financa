using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace app.Services
{
    public class EmailServices : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateEmailBody(string userName, string Password)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("TemplateEmail/htmlTemplate.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Message}", "Sistema automático de Recuperação de Senha.");
            body = body.Replace("{Password}", Password);
            body = body.Replace("{imagem}", "<img src='data:image/png;base64, " + ConvertImageTo64() + "' width='180px' height='50px'/>");
            return body;
        }

        private string ConvertImageTo64()
        {
            var teste = Convert.ToBase64String(System.IO.File.ReadAllBytes("TemplateEmail/logo-mlm.png"));
            /* return "data:image/png;base64, " + System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes("TemplateEmail/logo-mlm.png")); */
            return teste;
        }

        public async Task SendMail(string email, string username, string password)
        {

            var mail = new MailMessage();
            var client = new SmtpClient("leobo.heliohost.org", 587) //Port 8025, 587 and 25 can also be used.
            {
                Credentials = new NetworkCredential("leobo@hotmail.com", "VzxHicYg57w4"),
                EnableSsl = true
            };
            mail.From = new MailAddress("admin@mlmsistemas.com");
            mail.To.Add(email);
            mail.Subject = "Forgot Password";
            var plainView = AlternateView.CreateAlternateViewFromString("This is a text message", null, "text/plain");
            var htmlView = AlternateView.CreateAlternateViewFromString("This is a html message", null, "text/html");
            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);
            client.Send(mail);




            /* var mail = new MailMessage();
            var client = new SmtpClient("mail.smt2go.com", 587);

            client.Credentials = new NetworkCredential("leobo@hotmail.com", "VzxHicYg57w4");
            client.EnableSsl = true;
        
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress(_configuration["Email:Email"]);
            mail.Subject = "Redefinição de Senha";
            mail.Body = CreateEmailBody(username, password);
            mail.IsBodyHtml = true;
            client.Send(mail); */




            /* using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                    UserName = "leobogv@gmail.com",
                    Password = "camandocaia"
                };
                client.Credentials = credential;
                client.Host = "smtp.gmail.com";
                client.Port = 465;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                    emailMessage.Subject = "Redefinição de Senha";
                    emailMessage.Body = CreateEmailBody(username, password);
                    emailMessage.IsBodyHtml = true;
                    client.Send(emailMessage);
                }
            } */
            await Task.CompletedTask;
        }
    }
}