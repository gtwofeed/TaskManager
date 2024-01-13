namespace TaskManager.Api.Models
{
    public class CommpnModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] Photo { get; set; }
        public CommpnModel() => CreatedDate = DateTime.Now;
    }
}
