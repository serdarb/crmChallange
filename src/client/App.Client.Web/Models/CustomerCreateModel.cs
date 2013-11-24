using System.Collections.Generic;
using App.Utils;

namespace App.Client.Web.Models
{
    public class CustomerCreateModel: BaseModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public bool IsValid(CustomerCreateModel model)
        {
            return !string.IsNullOrEmpty(model.FirstName)
                   && !string.IsNullOrEmpty(model.LastName)
                   && !string.IsNullOrEmpty(model.Password)
                   && !string.IsNullOrEmpty(model.Email)
                   && model.Email.IsEmail();
        }
    }
}