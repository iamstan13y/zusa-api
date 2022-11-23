using System.ComponentModel.DataAnnotations.Schema;
using ZUSA.API.Enums;

namespace ZUSA.API.Models.Data
{
    public class Account
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [NotMapped]
        public string? Token { get; set; }
    }
}