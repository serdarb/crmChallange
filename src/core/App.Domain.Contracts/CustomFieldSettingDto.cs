using System.Collections.Generic;
using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CustomFieldSettingDto
    {
        [DataMember]
        public List<CustomFieldDto> CustomFieldDtos { get; set; }
        [DataMember]
        public string CompanyId { get; set; }
    }
}