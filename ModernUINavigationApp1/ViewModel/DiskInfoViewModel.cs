using FirstFloor.ModernUI.Presentation;

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

        public DiskInfoViewModel()
        {

        }

        public string[] SerialNumbers
        {
            get { return this._serialNumbers; }
        }
    }
}
