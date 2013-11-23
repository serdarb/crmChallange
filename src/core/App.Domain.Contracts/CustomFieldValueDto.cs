using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CustomFieldValueDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}