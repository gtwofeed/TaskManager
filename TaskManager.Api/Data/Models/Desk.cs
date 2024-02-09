using TaskManager.Api.Data.Models.Abstractions;

namespace TaskManager.Api.Data.Models
{
    public class Desk : CommonModel
    {
        public bool IsPrivate { get; set; }
        public string Colums { get; set; } = null!;
        public User Admin { get; set; } = null!;
        public Project Project { get; set; } = null!;
        public List<Task> Tasks { get; set; } = [];
    }
}
