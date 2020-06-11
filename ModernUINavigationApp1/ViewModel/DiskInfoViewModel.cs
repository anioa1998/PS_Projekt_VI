using FirstFloor.ModernUI.Presentation;
using ModernUINavigationApp1.Model;
using System.Runtime.CompilerServices;

namespace ModernUINavigationApp1.ViewModel
{

    public class DiskInfoViewModel : NotifyPropertyChanged
    {
        private string[] _serialNumbers = new string[]
        {
        "asmssmsm",
        "19292929-sssssss",
        "22222sass",
        "ddjidj-1112nssssssss"
        };

        private DiskInfoObject[] _diskData = new DiskInfoObject[]
        {
            new DiskInfoObject(){Name = "Size: ", Value="19.60 GB"},
            new DiskInfoObject(){Name = "Name: ", Value="PhysicalDisc0"},
            new DiskInfoObject(){Name = "Model: ", Value="Toshiba"},
            new DiskInfoObject(){Name = "Model: ", Value="Toshiba"},
            new DiskInfoObject(){Name = "Model: ", Value="Toshibabhvjhfgfhdghfj"},
            new DiskInfoObject(){Name = "Model: ", Value="Toshiba"},
            new DiskInfoObject(){Name = "Model: ", Value="Toshiba"},
            new DiskInfoObject(){Name = "Model: ", Value="Toshiba"}

        };
        public DiskInfoViewModel()
        {

        }

        public string[] SerialNumbers
        {
            get { return this._serialNumbers; }
        }
        public DiskInfoObject[] DiskData
        {
            get { return this._diskData; }
        }
    }
}
