namespace TaskManager.Common.Models
{
    public class TaskDTO : CommonDTO
    {
        public DeskDTO Desk { get; set; } = null!;
        public string Colum { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserDTO? Creater { get; set; }
        public UserDTO? Executor { get; set; }
    }
}
