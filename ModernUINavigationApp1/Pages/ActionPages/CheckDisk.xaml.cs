using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Services;
using ModernUINavigationApp1.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ModernUINavigationApp1.Pages.ActionPages
{
    /// <summary>
    /// Interaction logic for CheckDisk.xaml
    /// </summary>
    public partial class CheckDisk : UserControl
    {
        private Frame _navigationService;
        private CheckDiskViewModel _dataContext { get; set; }
        public CheckDisk(Frame navigationService, ConnectionService connectionService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _dataContext = new CheckDiskViewModel(connectionService);
            this.DataContext = _dataContext;
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

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "";
            if (!string.IsNullOrEmpty(cboxLogicalDisk.SelectedItem.ToString()))
            {
                txtResult.Text = _dataContext.ReturnStatus(cboxLogicalDisk.SelectedItem.ToString());
            }
        }
    }
}
