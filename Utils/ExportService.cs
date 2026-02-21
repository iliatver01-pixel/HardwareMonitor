using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HardwareMonitor.Models;
using HardwareMonitor.Services;
using HardwareMonitor.Utils;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;

namespace HardwareMonitor.Utils
{
    public class ExportService
    {
        public void ExportAll(object data)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "JSON (*.json)|*.json|Text (*.txt)|*.txt|CSV (*.csv)|*.csv";

            if (dialog.ShowDialog() == true)
            {
                string extension = Path.GetExtension(dialog.FileName);

                switch (extension)
                {
                    case ".json":
                        ExportToJson(dialog.FileName, data);
                        break;

                    case ".txt":
                        ExportToTxt(dialog.FileName, data);
                        break;

                    case ".csv":
                        ExportToCsv(dialog.FileName, data);
                        break;
                }
            }
        }

        private void ExportToJson(string path, object data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, json, Encoding.UTF8);
        }

        private void ExportToTxt(string path, object data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string content = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, content, Encoding.UTF8);
        }

        private void ExportToCsv(string path, object data)
        {
            var exportData = data as dynamic;

            var sb = new StringBuilder();

            sb.AppendLine("Section;Property;Value");

            sb.AppendLine($"CPU;Name;{exportData.CpuInfo.Name}");
            sb.AppendLine($"CPU;Cores;{exportData.CpuInfo.CoreCount}");
            sb.AppendLine($"CPU;Threads;{exportData.CpuInfo.ThreadCount}");
            sb.AppendLine($"CPU;Load %;{exportData.CpuInfo.LoadPercentage}");

            sb.AppendLine($"Memory;Total (GB);{exportData.MemoryInfo.TotalMemoryBytes / 1024.0 / 1024 / 1024}");
            sb.AppendLine($"Memory;Used %;{exportData.MemoryInfo.UsagePercentage}");

            sb.AppendLine($"Disk;Total (GB);{exportData.DiskInfo.LogicalDisks[0].TotalSizeBytes / 1024.0 / 1024 / 1024}");
            sb.AppendLine($"Disk;Free (GB);{exportData.DiskInfo.LogicalDisks[0].FreeSizeBytes / 1024.0 / 1024 / 1024}");

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }
    }
}