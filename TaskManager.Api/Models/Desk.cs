using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class Desk : CommonModel
    {
        public bool IsPrivate { get; set; }
        public string Colums { get; set; } = [];
        public required User Admin { get; set; }
        public required Project Project { get; set; }
        public List<Task> Tasks { get; set; } = [];
    }
}
