using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class User
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
        public List<Project> Projects { get; set; } = [];
        public List<Task> Tasks { get; set; } = [];
        public List<Desk> Desks { get; set; } = [];

        public User() { }
        public User(UserDTO userDTO)
        {
            Email = userDTO.Email;
            Password = userDTO.Password;
            Status = userDTO.Status;
            Phone = userDTO.Phone;
            Photo = userDTO.Photo;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
        }

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
