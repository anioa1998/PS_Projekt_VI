using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Services;
using ModernUINavigationApp1.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModernUINavigationApp1.Pages.ActionPages.DiskInfoPages
{
    /// <summary>
    /// Interaction logic for LogicalDisk.xaml
    /// </summary>
    public partial class LogicalDisk : UserControl
    {
        private Frame _navigationService;
        private LogicalDiskViewModel _dataContext;
        public LogicalDisk(Frame navigationService, ConnectionService connectionService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _dataContext = new LogicalDiskViewModel(connectionService);
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
            logicalDiskDataLV.ItemsSource = _dataContext.LogicalDiskData;
        }
    }
}
