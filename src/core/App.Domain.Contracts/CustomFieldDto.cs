using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CustomFieldDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DisplayNameEn { get; set; }
        [DataMember]
        public string DisplayNameTr { get; set; }
    }
}