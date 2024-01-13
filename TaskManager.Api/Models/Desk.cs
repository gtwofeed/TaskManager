namespace TaskManager.Api.Models
{
    public class Desk : CommpnModel
    {
        public bool IsPrivate { get; set; }
        public string Colums { get; set; }
        public User Creater { get; set; }
    }
}
