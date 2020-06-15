using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Services;
using ModernUINavigationApp1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ModernUINavigationApp1.Pages.ActionPages.DiskInfoPages
{
    /// <summary>
    /// Interaction logic for Partition.xaml
    /// </summary>
    public partial class Partition : UserControl
    {
        private Frame _navigationService;
        private PartitionViewModel _dataContext;
        public Partition(Frame navigationService, ConnectionService connectionService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _dataContext = new PartitionViewModel(connectionService);
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
            partitionDataLV.ItemsSource = _dataContext.PartitionData;
        }
    }
}
