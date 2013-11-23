using System.Collections.Generic;

namespace App.Domain
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string AdminEmail { get; set; }
        public string AdminId { get; set; }
        public List<CustomField> CustomFields { get; set; }
        public string Language { get; set; }
    }
}