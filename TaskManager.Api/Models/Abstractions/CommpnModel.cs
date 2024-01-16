namespace TaskManager.Api.Models
{
    public class CommpnModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[]? File { get; set; }
        public CommpnModel() => CreatedDate = DateTime.Now;
    }
}
