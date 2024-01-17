using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public UserStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public byte[]? Photo { get; set; }
        public DateTime? LastLoginDate { get; set; } = null;
    }
}
