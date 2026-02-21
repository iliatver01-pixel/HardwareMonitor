using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HardwareMonitor.Models;

namespace HardwareMonitor.Models
{
    public class DiskInfo
    {
        public List<PhysicalDiskInfo> PhysicalDisks { get; set; } = new List<PhysicalDiskInfo>();
        public List<LogicalDiskInfo> LogicalDisks { get; set; } = new List<LogicalDiskInfo>();
    }
}
