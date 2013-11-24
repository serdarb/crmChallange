using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class NameValueDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}