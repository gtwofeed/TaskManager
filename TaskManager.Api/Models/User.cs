using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public UserStatus Status { get; set; }
        public required DateTime RegistrationDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public byte[]? Photo { get; set; }
        public DateTime? LastLoginDate { get; set; } = null;
        public List<Project> Projects { get; set; } = [];
        public List<Task> Tasks { get; set; } = [];
        public List<Desk> Desks { get; set; } = [];

        public User() { }

        public UserDTO ToDTO() =>
            new()
            { 
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Phone = Phone,
                RegistrationDate = RegistrationDate,
                LastLoginDate = LastLoginDate,
                Photo = Photo,
                Status = Status,
            };
    }
}
