namespace TaskManager.Api.Models
{
    public class Project : CommpnModel
    {
        public int? AdminId { get; set; }
        public User? Admin {  get; set; }
        public List<User> Users { get; set; } = [];
        public List<Desk> Desks { get; set; } = [];
        public ProjectStatus Status { get; set; }
    }
}
