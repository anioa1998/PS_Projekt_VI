using FirstFloor.ModernUI.Presentation;
using ModernUINavigationApp1.Model;
using ModernUINavigationApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ModernUINavigationApp1.ViewModel
{
    public class LogicalDiskViewModel : NotifyPropertyChanged
    {
        private string[] _logicalDiskNames;
        private Dictionary<string, List<DiskInfoObject>> _allLogicalDiskData = new Dictionary<string, List<DiskInfoObject>>();

        private DiskInfoObject[] _logicalDiskData;
        private ManagementObjectCollection _queryCollection { get; set; }
        private ConnectionService _connectionService { get; set; }

        public LogicalDiskViewModel(ConnectionService connectionService)
        {
            _connectionService = connectionService;
            _logicalDiskNames = GetLogicalDiskNames();
        }

        public string[] LogicalDiskNames
        {
            get { return this._logicalDiskNames; }
        }
        public DiskInfoObject[] LogicalDiskData
        {
            get { return this._logicalDiskData; }
        }
        private string[] GetLogicalDiskNames()
        {
            List<string> logicalDiskNames = new List<string>();

            try
            {
                _queryCollection = _connectionService.GetQueryCollectionFromWin32Class("Win32_LogicalDisk");


                foreach (ManagementObject logicalDiskData in _queryCollection)
                {

                    List<DiskInfoObject> infoObjects = new List<DiskInfoObject>();
                    string logicalDiskName = logicalDiskData["Name"].ToString();

                    logicalDiskNames.Add(logicalDiskName);

                    infoObjects.Add(new DiskInfoObject() { Name = "Description: ", Value = logicalDiskData["Description"].ToString() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Size: ", Value = logicalDiskData["Size"].ToString().ToGB() });
                    infoObjects.Add(new DiskInfoObject() { Name = "File system: ", Value = logicalDiskData["FileSystem"].ToString() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Free space: ", Value = logicalDiskData["FreeSpace"].ToString().ToGB() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Volume name: ", Value = logicalDiskData["VolumeName"].ToString() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Serial number: ", Value = logicalDiskData["VolumeSerialNumber"].ToString() });

                    _allLogicalDiskData.Add(logicalDiskName, infoObjects);
                }
                return logicalDiskNames.ToArray();

            }
            catch (Exception e)
            {
                Console.WriteLine("LogicalDiskInformation Error: " + e.Message);
                return logicalDiskNames.ToArray();
            }

        }

        public void SetData(string logicalDiskName)
        {
            List<DiskInfoObject> diskInfoObjects = new List<DiskInfoObject>();
            diskInfoObjects = _allLogicalDiskData.Where(x => x.Key == logicalDiskName)
                                               .Select(x => x.Value)
                                               .Single();
            _logicalDiskData = diskInfoObjects.ToArray();
        }

    }
}
