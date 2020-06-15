using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.S.M.A.R.T;
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

namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for Smart.xaml
    /// </summary>
    public partial class Smart : UserControl
    {
        private Frame _navigationService;
        private SMARTViewModel _dataContext;

        public Smart(Frame navigationService, ManagementScope scope)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _dataContext = new SMARTViewModel();

            ISmartBuilder builder = new SmartBuilder();
            builder.SetScope(scope)
                   .SetDriveStorage()
                   .SetViewModel(_dataContext)
                   .Build();
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
        private void lstViewSerial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selected = (string)lstViewSerial.SelectedItem;
            _dataContext.SetData(selected);
            smartDataLV.ItemsSource = _dataContext.SmartData;
        }


    }
}
