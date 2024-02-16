using TaskManager.Common.Models;

namespace TaskManager.Api.Data.Models.Abstractions
{
    public class CommonModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }

        public CommonModel() =>
            CreatedDate = DateTime.Now;
        public CommonModel(CommonDTO dto) : this()
        {
            Name = dto.Name;
            Description = dto.Description;
            Photo = dto.Photo;
        }
    }
}
