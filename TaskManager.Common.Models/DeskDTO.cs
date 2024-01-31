using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Common.Models.Abstractions;

namespace TaskManager.Common.Models
{
    public class DeskDTO : CommonDTO
    {
        public bool IsPrivate { get; set; }
        public string[] Colums { get; set; } = [];
        public UserDTO Admin { get; set; } = null!;
        public ProjectDTO Project { get; set; } = null!;
        public List<TaskDTO> Tasks { get; set; } = [];
    }
}
