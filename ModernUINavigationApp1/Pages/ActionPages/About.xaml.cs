using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;

namespace ModernUINavigationApp1.Pages.ActionPages
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        private Frame _navigationService;

        public About()
        {
            InitializeComponent();
        }
        public About(Frame navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            txtAbout.Text = @"DiskManagerApplication with GUI is a simple application that allows you to get basic information about HDD, SSD 
or FLASH data and their partitions. It also allows the option of reading SMART properties and repairing the system disk with CheckDisk (also 
with the option of enabling after restart). It is possible to format the selected partition on a given data storage and search for active hosts
in our local network.

Authors: Anna Piechowska & Sebastian Franc IP31";
        }

        private void btnReturn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
            }
            else
            {
                ModernDialog.ShowMessage("No entries in back navigation history.", "navigate", MessageBoxButton.OK);
            }
        }
    }
}
