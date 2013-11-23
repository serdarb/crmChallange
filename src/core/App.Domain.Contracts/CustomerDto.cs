using System.Collections.Generic;
using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CustomerDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CreatedBy { get; set; }
        public List<CustomFieldValueDto> CustomFieldValues { get; set; }
    }
}