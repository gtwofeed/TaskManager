using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Models
{
    public class ProjectDTO : CommonDTO
    {
        public int? AdminId { get; set; }
        public List<UserDTO> Users { get; set; } = [];
        public List<DeskDTO> Desks { get; set; } = [];
        public ProjectStatus Status { get; set; }
        public ProjectDTO() { }
    }
}
 