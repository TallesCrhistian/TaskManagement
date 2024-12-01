using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.UI.Enumerators;

namespace TaskManagement.UI.ViewModels
{
    public class TaskCreateViewModel
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public EnumTaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
