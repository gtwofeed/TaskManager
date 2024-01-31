using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Models
{
    public class DeskDTO : CommonDTO
    {
        public bool IsPrivate { get; set; }
        public string[] Colums { get; set; } = [];
        public required UserDTO Admin { get; set; }
        public required ProjectDTO Project { get; set; }
        public List<TaskDTO> Tasks { get; set; } = [];
    }
}
