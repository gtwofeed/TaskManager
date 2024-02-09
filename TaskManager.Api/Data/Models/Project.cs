using TaskManager.Api.Data.Models.Abstractions;
using TaskManager.Common.Models;

namespace TaskManager.Api.Data.Models
{
    public class Project : CommonModel
    {
        public ProjectStatus Status { get; set; }
        public User? Admin { get; set; }
        public List<User> Users { get; set; } = [];
        public List<Desk> Desks { get; set; } = [];

        public Project() { }
        public Project(ProjectDTO dto) : base(dto)
        {
            Status = dto.Status;
        }

        public ProjectDTO ToDTO() =>
            new()
            {
                Id = Id,
                Admin = Admin?.ToDTO(),
                Name = Name,
                CreatedDate = CreatedDate,
                Status = Status,
                Description = Description,
                Photo = Photo,
            };
    }
}
