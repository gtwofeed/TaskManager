namespace TaskManager.Api.Models
{
    public class Project : CommpnModel
    {
        public List<User> Users { get; set; }
        public List<Desk> Desks { get; set; }
    }
}
