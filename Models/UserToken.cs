using System;

namespace app.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public Int64 Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}