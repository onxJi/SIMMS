using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models
{
    public class ActivityModel
    {
        public long Id { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}
