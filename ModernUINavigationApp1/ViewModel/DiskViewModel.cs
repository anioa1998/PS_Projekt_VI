using FirstFloor.ModernUI.Presentation;
using ModernUINavigationApp1.Model;
using ModernUINavigationApp1.Pages.ActionPages.DiskInfoPages;
using ModernUINavigationApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernUINavigationApp1.ViewModel
{

    public class DiskViewModel : NotifyPropertyChanged
    {

        [DllImport(@"C:\Users\Ania\source\repos\ModernUINavigationApp1\PS_Projekt_VI\ModernUINavigationApp1\Win32_DiskInfo_CPP.dll", EntryPoint = "disk_info", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ShowDiskInfo(string name, int strlen, StringBuilder str);

        private string[] _diskNames;
        private Dictionary<string, string> _nameToSerial = new Dictionary<string, string>();
        private Dictionary<string, List<DiskInfoObject>> _allDiskData = new Dictionary<string, List<DiskInfoObject>>();

        private DiskInfoObject[] _diskData;
        //{
        //    new DiskInfoObject(){Name = "Size: ", Value="19.60 GB"},
        //    new DiskInfoObject(){Name = "Name: ", Value="PhysicalDisc0"},
        //    new DiskInfoObject(){Name = "Model: ", Value="Toshiba"},
        //    new DiskInfoObject(){Name = "Model: ", Value="Toshiba"},
        //    new DiskInfoObject(){Name = "Model: ", Value="Toshibabhvjhfgfhdghfj"},
        //    new DiskInfoObject(){Name = "Model: ", Value="Toshiba"},
        //    new DiskInfoObject(){Name = "Model: ", Value="Toshiba"},
        //    new DiskInfoObject(){Name = "Model: ", Value="Toshiba"}

        //};
        private ManagementObjectCollection _queryCollection { get; set; }
        private ManagementScope _scope { get; set; }
        private ConnectionService _connectionService { get; set; }
        public DiskViewModel(ManagementScope scope, ConnectionService connectionService)
        {
            _scope = scope;
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

                    //Console.WriteLine("---------------------------------------------");
                    
                    List<DiskInfoObject> infoObjects = new List<DiskInfoObject>();
                    string diskName = diskData["Name"].ToString();

                    diskNames.Add(diskName);
                    _nameToSerial.Add(diskName, diskData["SerialNumber"].ToString() );

                    infoObjects.Add(new DiskInfoObject() { Name = "Model: ", Value = diskData["Model"].ToString() });

                    _allDiskData.Add(diskName, infoObjects);
                    //Console.WriteLine("Model: {0}", diskData["Model"]);
                    //Console.WriteLine("Rozmiar: {0} GB", ConvertToGB(Convert.ToInt64(diskData["Size"])));
                    //Console.WriteLine("Numer seryjny {0}", diskData["SerialNumber"]);
                    //Console.WriteLine("Całkowita ilość sektorów: {0}", diskData["TotalSectors"]);
                    //Console.WriteLine("Status: {0}", diskData["Status"]);
                    //Console.WriteLine("Typ interfejsu: {0}", diskData["InterfaceType"]);
                    //Console.WriteLine("Rodzaj pamięci: {0}", diskData["MediaType"]);
                    //Console.WriteLine("Rewizja Firmware: {0}", diskData["FirmwareRevision"]);
                    //Console.WriteLine("Opis zdolności:");
                    //IEnumerable enumerable = diskData["CapabilityDescriptions"] as IEnumerable;
                    //if (enumerable != null)
                    //{
                    //    foreach (object element in enumerable)
                    //    {
                    //        Console.WriteLine("- " + element);
                    //    }
                    //}

                    //Console.WriteLine("---------------------------------------------\n");
                }
                return diskNames.ToArray();

            }
            catch (Exception e)
            {
                Console.WriteLine("DiskPartitionInformation Error: " + e.Message);
                return diskNames.ToArray();
            }

        }

        public void SetData(string diskName)
        {
            List<DiskInfoObject> diskInfoObjects = new List<DiskInfoObject>();
            //_allDiskData.TryGetValue(diskName, out diskInfoObjects);
            //diskInfoObjects = _allDiskData.Where(x => x.Key == diskName)
            //                              .Select(x => x.Value)
            //                              .Single();
            //_diskData = diskInfoObjects.ToArray();
            string serialNumber = _nameToSerial.Where(x => x.Key == diskName)
                                               .Select(x => x.Value)
                                               .Single();
            string query = "SELECT * FROM Win32_DiskDrive WHERE SerialNumber LIKE '%" + serialNumber + "%'";
            StringBuilder str = new StringBuilder(2048);
            ShowDiskInfo(query, 2048, str);

            string dataToSplit = str.ToString();

            string[] values = dataToSplit.Split(';');


            diskInfoObjects.Add(new DiskInfoObject() { Name = "Model: ", Value = values[0] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Size: ", Value = values[1].ToGB()}) ;
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Sectors: ", Value = values[2] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Status: ", Value = values[3] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Interface: ", Value = values[4] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Media: ", Value = values[5] });
            diskInfoObjects.Add(new DiskInfoObject() { Name = "Firmware: ", Value = values[6] });

            _diskData = diskInfoObjects.ToArray();
        }
        //private DiskInfoObject[] SetDiskData()
    }
}
