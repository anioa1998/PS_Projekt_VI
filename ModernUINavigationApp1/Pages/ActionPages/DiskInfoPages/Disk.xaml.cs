using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Services;
using ModernUINavigationApp1.ViewModel;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModernUINavigationApp1.Pages.ActionPages.DiskInfoPages
{
    /// <summary>
    /// Interaction logic for Disk.xaml
    /// </summary>
    public partial class Disk : UserControl
    {
        private Frame _navigationService;
        private DiskViewModel _dataContext;
        public Disk(Frame navigationService, ManagementScope scope, ConnectionService connectionService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _dataContext = new DiskViewModel(scope, connectionService); //to use methods in DiskInfoViewModel outside
            DataContext = _dataContext;

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

        private void lstViewNames_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selected = (string)lstViewNames.SelectedItem;
            _dataContext.SetData(selected);
            DiskDataLV.ItemsSource = _dataContext.DiskData;

        }
    }
}
