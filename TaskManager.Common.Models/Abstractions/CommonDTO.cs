namespace TaskManager.Common.Models
{
    public class CommonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }
    }
}
