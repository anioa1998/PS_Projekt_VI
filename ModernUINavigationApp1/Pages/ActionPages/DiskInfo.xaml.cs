using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Pages.ActionPages.DiskInfoPages;
using ModernUINavigationApp1.Services;
using ModernUINavigationApp1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernUINavigationApp1.Pages.ActionPages
{
    /// <summary>
    /// Interaction logic for DiskInfo.xaml
    /// </summary>
    public partial class DiskInfo : UserControl
    {
        private Frame _navigationService { get; set; }
        private DiskViewModel _dataContext { get; set; }
        private ManagementScope _scope { get; set; }
        private ConnectionService _connectionService { get; set; }
        public DiskInfo(Frame navigationService, ManagementScope scope, ConnectionService connectionService) 
        {
            InitializeComponent();
            _navigationService = navigationService;
            _scope = scope;
            _connectionService = connectionService;
            //_dataContext = new DiskInfoViewModel(scope, connectionService);
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
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

        private void btnDisk_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new Disk(_navigationService, _connectionService));
        }

        private void btnPartition_Click(object sender, RoutedEventArgs e)
        {
             _navigationService.Navigate(new Partition(_navigationService, _connectionService));
        }

        private void btnLogicalDisk_Click(object sender, RoutedEventArgs e)
        {
            //_navigationService.Navigate(new LogicalDisk(_navigationService, _dataContext));
        }
    }
}
