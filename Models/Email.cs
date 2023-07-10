namespace app.Models
{
    public class Email
    {
        public string toname {get;set;}
        public string toemail { get; set; }
        /* public const string subject = "Recuperação de Senha"; */
        public string message { get; }
        public string Password { get; set; }

        public Email(string email, string nome, string password)
        {
            toemail = email;
            message = "Recuperação de Senha";
            toname = nome;
            Password = password;
        }
    }
}