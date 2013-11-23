using System.Collections.Generic;
using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CompanyDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string AdminEmail { get; set; }
        [DataMember]
        public string AdminId { get; set; }
        [DataMember]
        public List<CustomFieldDto> CustomFields { get; set; }
        [DataMember]
        public string Language { get; set; }
    }

    
}