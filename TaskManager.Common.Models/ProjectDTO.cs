namespace TaskManager.Common.Models
{
    public class ProjectDTO : CommonDTO
    {
        public UserDTO? Admin { get; set; }
        public List<UserDTO> Users { get; set; } = [];
        public List<DeskDTO> Desks { get; set; } = [];
        public ProjectStatus Status { get; set; }
        public ProjectDTO() { }
    }
}
 