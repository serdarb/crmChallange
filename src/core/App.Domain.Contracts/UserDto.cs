using System.Runtime.Serialization;

namespace App.Domain.Contracts
{
    [DataContract]
    public class UserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Lang { get; set; }
    }
}
