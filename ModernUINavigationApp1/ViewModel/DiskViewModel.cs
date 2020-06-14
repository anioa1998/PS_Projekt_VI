using FirstFloor.ModernUI.Presentation;
using ModernUINavigationApp1.Model;
using ModernUINavigationApp1.Pages.ActionPages.DiskInfoPages;
using ModernUINavigationApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ModernUINavigationApp1.ViewModel
{

    public class DiskViewModel : NotifyPropertyChanged
    {

        [DllImport(@"C:\Users\SebFra\Desktop\vsdvwrvrgre\PS_Projekt_VI\ModernUINavigationApp1\Win32_DiskInfo_CPP.dll", EntryPoint = "disk_info", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ShowDiskInfo(string name, int strlen, StringBuilder str);

        private string[] _diskNames;
        private Dictionary<string, string> _nameToSerial = new Dictionary<string, string>();
        private Dictionary<string, List<DiskInfoObject>> _allDiskData = new Dictionary<string, List<DiskInfoObject>>();

        private DiskInfoObject[] _diskData;
        private ManagementObjectCollection _queryCollection { get; set; }
        private ConnectionService _connectionService { get; set; }
        public DiskViewModel(ConnectionService connectionService)
        {
            _connectionService = connectionService;
            _diskNames = GetDiskNames();

        }

        public string[] DiskNames
        {
            get { return this._diskNames; }
        }
        public DiskInfoObject[] DiskData
        {
            get { return this._diskData; }
        }
        private string[] GetDiskNames()
        {
            List<string> diskNames = new List<string>();

            try
            {
                _queryCollection = _connectionService.GetQueryCollectionFromWin32Class("Win32_DiskDrive");

                foreach (ManagementObject diskData in _queryCollection)
                {

                    List<DiskInfoObject> infoObjects = new List<DiskInfoObject>();
                    string diskName = diskData["Name"].ToString();

                    diskNames.Add(diskName);
                    _nameToSerial.Add(diskName, diskData["SerialNumber"].ToString());
                 
                }
                return diskNames.ToArray();

            }
            catch (Exception e)
            {
                Console.WriteLine("DiskInformation Error: " + e.Message);
                return diskNames.ToArray();
            }

        }

        public void SetData(string diskName)
        {
            List<DiskInfoObject> diskInfoObjects = new List<DiskInfoObject>();
            
            string serialNumber = _nameToSerial.Where(x => x.Key == diskName)
                                               .Select(x => x.Value)
                                               .Single();

            Thread getInfoThread = new Thread(() => GetInfoAboutDisk(diskInfoObjects, serialNumber));

            getInfoThread.Start();

        }

        public void GetInfoAboutDisk(List<DiskInfoObject> diskInfoObjects, string serialNumber)
        {
            StringBuilder str = new StringBuilder(2048);
            string query = "SELECT * FROM Win32_DiskDrive WHERE SerialNumber LIKE '%" + serialNumber + "%'";

            ShowDiskInfo(query, 2048, str);

            string dataToSplit = str.ToString();

            string[] values = dataToSplit.Split(';');


            diskInfoObjects.Add(new DiskInfoObject() { Name = "Model: ", Value = values[0] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Size: ", Value = values[1].ToGB() });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Sectors: ", Value = values[2] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Status: ", Value = values[3] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Interface: ", Value = values[4] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Media: ", Value = values[5] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Firmware: ", Value = values[6] });

            _diskData = diskInfoObjects.ToArray();
        }

    }
}
