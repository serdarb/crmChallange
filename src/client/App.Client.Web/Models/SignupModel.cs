using App.Utils;

namespace App.Client.Web.Models
{
    public class SignupModel : BaseModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }

        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }

        public bool IsValid(SignupModel model)
        {
            return !string.IsNullOrEmpty(model.FirstName)
                   && !string.IsNullOrEmpty(model.LastName)
                   && !string.IsNullOrEmpty(model.Language)
                   && !string.IsNullOrEmpty(model.Password)
                   && !string.IsNullOrEmpty(model.CompanyName)
                   && !string.IsNullOrEmpty(model.CompanyUrl)
                   && !string.IsNullOrEmpty(model.Email)
                   && model.Email.IsEmail();
        }
    }
}