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
    }
}
