using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class Project : CommonModel
    {
        public ProjectStatus Status { get; set; }
        //public int? AdminId { get; set; }
        public User? Admin {  get; set; }
        public List<User> Users { get; set; } = [];
        public List<Desk> Desks { get; set; } = [];

        public Project() { }
        public Project(ProjectDTO dto, User admin) : base(dto)
        {
            Status = dto.Status;
            Admin = admin;
        }

        public ProjectDTO ToDTO() =>
            new ProjectDTO
            {
                Id = Id,
                AdminId = Admin?.Id,
                Name = Name,
                CreatedDate = CreatedDate,
                Status = Status,
                Description = Description,
                Photo = Photo,
            };
    }
}
