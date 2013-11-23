using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CustomFieldValueDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}