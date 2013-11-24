using System.Collections.Generic;
using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CustomerDto
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public string CompanyId { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public List<NameValueDto> CustomFieldValues { get; set; }
    }
}