namespace App.Domain
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
    }
}
