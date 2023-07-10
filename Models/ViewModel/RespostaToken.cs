namespace app.Models.ViewModel
{
    public class RespostaToken
    {
        public string Mensagem { get; set; }
        public bool Status { get; set; }

        public static RespostaToken Result(string _mensagem, bool _status)
        {
            return new RespostaToken{
                Mensagem = _mensagem,
                Status = _status
            };
        }
    }

    
}