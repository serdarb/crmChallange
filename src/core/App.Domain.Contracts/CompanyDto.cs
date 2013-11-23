using System.Collections.Generic;
using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class CompanyDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string AdminEmail { get; set; }
        public string AdminId { get; set; }
        public List<string> CustomFields { get; set; }
        public string Language { get; set; }
    }
}