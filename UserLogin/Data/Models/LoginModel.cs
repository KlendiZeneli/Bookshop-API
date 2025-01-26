namespace UserLogin.Data.Models
{
    public class LoginModel
    {
        public string Identifier { get; set; }// Username or Email
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    }

