namespace TaskManager.Api.Models
{
    public class User
    {
        public int Id {  get; set; }
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set;}
        public byte[]? Photo { get; set; }
        public List<Project> Projects { get; set; } = [];
        public List<Task> Tasks { get; set; } = [];
        public List<Desk> Desks { get; set; } = [];
        public UserStatus Status { get; set; }
    }
}
