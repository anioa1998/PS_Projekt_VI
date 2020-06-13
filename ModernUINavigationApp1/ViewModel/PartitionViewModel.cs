using FirstFloor.ModernUI.Presentation;
using ModernUINavigationApp1.Model;
using ModernUINavigationApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace ModernUINavigationApp1.ViewModel
{
    public class PartitionViewModel : NotifyPropertyChanged
    {
        private string[] _partitionNames;
        private Dictionary<string, List<DiskInfoObject>> _allPartitionData = new Dictionary<string, List<DiskInfoObject>>();

        private DiskInfoObject[] _partitionData;
        private ManagementObjectCollection _queryCollection { get; set; }
        private ConnectionService _connectionService { get; set; }

        public PartitionViewModel(ConnectionService connectionService)
        {
            _connectionService = connectionService;
            _partitionNames = GetPartitionNames();
        }

        public string[] PartitionNames
        {
            get { return this._partitionNames; }
        }
        public DiskInfoObject[] PartitionData
        {
            get { return this._partitionData; }
        }
        private string[] GetPartitionNames()
        {
            List<string> partitionNames = new List<string>();

            try
            {
                _queryCollection = _connectionService.GetQueryCollectionFromWin32Class("Win32_DiskPartition");


                foreach (ManagementObject partitionData in _queryCollection)
                {

                    List<DiskInfoObject> infoObjects = new List<DiskInfoObject>();
                    string partitionName = partitionData["Name"].ToString();

                    partitionNames.Add(partitionName);

                    infoObjects.Add(new DiskInfoObject() { Name = "Disk index: ", Value = partitionData["DiskIndex"].ToString() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Size: ", Value = partitionData["Size"].ToString().ToGB() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Number of blocks: ", Value = partitionData["NumberOfBlocks"].ToString() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Block size: ", Value = partitionData["BlockSize"].ToString() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Type: ", Value = partitionData["Type"].ToString() });
                    infoObjects.Add(new DiskInfoObject() { Name = "Is it bootable?: ", Value = partitionData["Bootable"].ToString() });

                    _allPartitionData.Add(partitionName, infoObjects);
                }
                return partitionNames.ToArray();

            }
            catch (Exception e)
            {
                Console.WriteLine("PartitionInformation Error: " + e.Message);
                return partitionNames.ToArray();
            }

        }

        public void SetData(string diskName)
        {
            List<DiskInfoObject> diskInfoObjects = new List<DiskInfoObject>();
            diskInfoObjects = _allPartitionData.Where(x => x.Key == diskName)
                                               .Select(x => x.Value)
                                               .Single();
            _partitionData = diskInfoObjects.ToArray();
        }
    }
}
