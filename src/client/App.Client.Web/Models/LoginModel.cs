using App.Utils;

namespace App.Client.Web.Models
{
    public class LoginModel : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }

        public bool IsValid(LoginModel model)
        {
            return !string.IsNullOrEmpty(model.Password)
                   && !string.IsNullOrEmpty(model.Email)
                   && model.Email.IsEmail();
        }
    }
}