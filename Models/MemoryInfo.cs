using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.Models
{
    public class MemoryInfo
    {
        public long TotalMemoryBytes { get; set; }
        public long AvailableMemoryBytes { get; set; }
        public double UsagePercentage { get; set; }
        public List<MemoryModuleInfo> Modules { get; set; } = new List<MemoryModuleInfo>();
    }
}
