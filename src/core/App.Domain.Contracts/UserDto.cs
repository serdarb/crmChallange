using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Language { get; set; }
    }
}
