using System;
using System.Reflection;

namespace TaskManager.Common.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public byte[]? Photo { get; set; }
        public DateTime? LastLoginDate { get; set; } = null;

        public override bool Equals(object? obj)
        {
            if (obj is UserDTO user)
            {
                if (user.Id != Id) return false;
                else if (Email != user.Email) return false;
                else if (Password != user.Password) return false;
                else if (Status != user.Status) return false;
                else if (RegistrationDate != user.RegistrationDate) return false;
                else if (FirstName != user.FirstName) return false;
                else if (LastName != user.LastName) return false;
                else if (Phone != user.Phone) return false;
                else if (Photo != null && user.Photo == null) return false;
                else if (Photo == null && user.Photo != null) return false;
                else if (Photo != null && user.Photo != null && !user.Photo.SequenceEqual(user.Photo)) return false;
                return true;
            }
            return false;
        }
    }
}
