using FirstFloor.ModernUI.Windows.Controls;
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
