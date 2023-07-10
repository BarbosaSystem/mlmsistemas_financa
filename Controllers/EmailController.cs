using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("send-email")]
        public async Task SendMail(Email objData)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(objData.toemail));
            message.From = new MailAddress(_configuration["Email:Email"]);
            /* message.Bcc.Add(new MailAddress("Amit Mohanty <leobo@hotmail.com>")); */
            message.Subject = "Recuperação de Senha";
            message.Body = createEmailBody(objData.toname, objData.message, objData.Password);
            message.IsBodyHtml = true;


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                smtp.Credentials = credential;
                smtp.Host = _configuration["Email:Host"];
                smtp.Port = int.Parse(_configuration["Email:Port"]);
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(message);
                await Task.FromResult(0);
            }
        }

        private string ConvertImageTo64()
        {
            var teste = Convert.ToBase64String(System.IO.File.ReadAllBytes("TemplateEmail/logo-mlm.png"));
            /* return "data:image/png;base64, " + System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes("TemplateEmail/logo-mlm.png")); */
            return teste;
        }
        private string createEmailBody(string userName, string message, string Password)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("TemplateEmail/htmlTemplate.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Message}", message);
            body = body.Replace("{Password}", Password);
            body = body.Replace("{imagem}", "<img src='data:image/png;base64, " + ConvertImageTo64() + "' width='180px' height='50px'/>" );
            return body;
        }
    }
}