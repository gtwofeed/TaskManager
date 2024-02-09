using TaskManager.Api.Data.Models.Abstractions;

namespace TaskManager.Api.Data.Models
{
    public class Task : CommonModel
    {
        public Desk Desk { get; set; } = null!;
        public string Colum { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[]? File { get; set; }
        public User? Creater { get; set; }
        public User? Executor { get; set; }
    }
}
