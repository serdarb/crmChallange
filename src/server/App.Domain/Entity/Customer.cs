using System.Collections.Generic;

namespace App.Domain
{
    public class Customer : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<NameValue> CustomFieldValues { get; set; }
    }
}