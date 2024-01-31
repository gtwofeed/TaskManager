using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class Task : CommonModel
    {
        public required Desk Desk { get; set; }
        public string Colum { get; set; } = [];
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User? Creater { get; set; }
        public User? Executor { get; set; }
    }
}
