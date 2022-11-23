using ZUSA.API.Enums;

namespace ZUSA.API.Models.Local
{
    public class AccountRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public int SchoolId { get; set; }
        public Role Role { get; set; }
    }
}