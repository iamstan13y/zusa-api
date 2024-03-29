﻿using System.ComponentModel.DataAnnotations.Schema;
using ZUSA.API.Enums;

namespace ZUSA.API.Models.Data
{
    public class Account
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public int SchoolId { get; set; }
        public bool IsActive { get; set; } = false;
        public Role Role { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public School? School { get; set; }
        [NotMapped]
        public string? Token { get; set; }
    }
}