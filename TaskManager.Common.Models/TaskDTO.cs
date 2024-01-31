using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Models
{
    public class TaskDTO : CommonDTO
    {
        public required DeskDTO Desk { get; set; }
        public string Colum { get; set; } = [];
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserDTO? Creater { get; set; }
        public UserDTO? Executor { get; set; }
    }
}
