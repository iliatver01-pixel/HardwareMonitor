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

namespace HardwareMonitor.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly CpuMonitor _cpuMonitor = new CpuMonitor();
        private readonly MemoryMonitor _memoryMonitor = new MemoryMonitor();
        private readonly DiskMonitor _diskMonitor = new DiskMonitor();
        private readonly ExportService _exportService = new ExportService();

        private DispatcherTimer _timer;

        public CpuInfo CpuInfo { get; set; }
        public MemoryInfo MemoryInfo { get; set; }
        public DiskInfo DiskInfo { get; set; }

        public ICommand RefreshCommand { get; }
        public ICommand ExportCommand { get; }

        public MainViewModel()
        {
            RefreshCommand = new RelayCommand(async () => await RefreshAsync());
            ExportCommand = new RelayCommand(ExportData);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(3);
            _timer.Tick += async (s, e) => await RefreshAsync();
            _timer.Start();
        }

        private async Task RefreshAsync()
        {
            CpuInfo = await Task.Run(() => _cpuMonitor.GetCpuInfo());
            MemoryInfo = await Task.Run(() => _memoryMonitor.GetMemoryInfo());
            DiskInfo = await Task.Run(() => _diskMonitor.GetDiskInfo());

            OnPropertyChanged(nameof(CpuInfo));
            OnPropertyChanged(nameof(MemoryInfo));
            OnPropertyChanged(nameof(DiskInfo));
        }

        private void ExportData()
        {
            var data = new
            {
                CpuInfo,
                MemoryInfo,
                DiskInfo,
                Date = DateTime.Now
            };

            _exportService.ExportAll(data);
        }
    }
}