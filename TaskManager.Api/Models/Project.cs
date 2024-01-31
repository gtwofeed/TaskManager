using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class Project : CommonModel
    {
        public int? AdminId { get; set; }
        public User? Admin {  get; set; }
        public List<User> Users { get; set; } = [];
        public List<Desk> Desks { get; set; } = [];
        public ProjectStatus Status { get; set; }
        public Project() { }
        public Project(ProjectDTO dto) : base(dto)
        {
            Status = dto.Status;
            AdminId = dto.AdminId;
        }
    }
}
