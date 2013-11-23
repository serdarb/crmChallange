using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CustomFieldDto
    {
        public string Name { get; set; }
        public string DisplayNameEn { get; set; }
        public string DisplayNameTr { get; set; }
    }
}