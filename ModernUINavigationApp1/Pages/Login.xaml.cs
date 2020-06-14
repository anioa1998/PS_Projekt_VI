using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;

namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private Frame _navigationService;
        private string _computerName;
        public Login(Frame navigationService, string computerName)
        {
            InitializeComponent();
            _computerName = computerName;
            _navigationService = navigationService;
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtLogin.Text;
            string password = txtPassword.Password;

            _navigationService.Navigate(new MainMenu(_navigationService,_computerName,username,password));
        }
    }
}
