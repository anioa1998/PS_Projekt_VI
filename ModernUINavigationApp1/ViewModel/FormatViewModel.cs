using FirstFloor.ModernUI.Presentation;
using ModernUINavigationApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ModernUINavigationApp1.ViewModel
{
    public class FormatViewModel : NotifyPropertyChanged
    {
        private string[] _logicalDiskNames;
        private string[] _fileSystems = new string[]
        {
            "NTFS",
            "FAT32"
        };
        private ManagementObjectCollection _queryCollection { get; set; }
        private ConnectionService _connectionService { get; set; }

        public FormatViewModel(ConnectionService connectionService)
        {
            _connectionService = connectionService;
            _logicalDiskNames = GetLogicalDiskNames();
        }

        public string[] LogicalDiskNames
        {
            get { return this._logicalDiskNames; }
        }
        public string[] FileSystems
        {
            get { return this._fileSystems; }
        }

        private string[] GetLogicalDiskNames()
        {
            List<string> logicalDiskNames = new List<string>();

            try
            {
                _queryCollection = _connectionService.GetQueryCollectionFromWin32Class("Win32_LogicalDisk");


                foreach (ManagementObject logicalDiskData in _queryCollection)
                {
                    string logicalDiskName = logicalDiskData["Name"].ToString();
                    logicalDiskNames.Add(logicalDiskName);
                }
                return logicalDiskNames.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine("FormatLogicalDiskInformation Error: " + e.Message);
                return logicalDiskNames.ToArray();
            }
        }
    }
}
