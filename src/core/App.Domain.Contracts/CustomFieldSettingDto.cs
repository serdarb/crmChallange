using System.Collections.Generic;
using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CustomFieldSettingDto
    {
        public List<CustomFieldDto> CustomFieldDtos { get; set; }
        public string CompanyId { get; set; }
    }
}