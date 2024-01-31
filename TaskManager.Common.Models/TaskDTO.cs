using TaskManager.Common.Models.Abstractions;

namespace TaskManager.Common.Models
{
    public class TaskDTO : CommonDTO
    {
        public required DeskDTO Desk { get; set; }
        public string Colum { get; set; } = [];
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserDTO? Creater { get; set; }
        public UserDTO? Executor { get; set; }
    }
}
