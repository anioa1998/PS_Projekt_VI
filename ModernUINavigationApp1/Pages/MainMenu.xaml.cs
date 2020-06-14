using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Pages.ActionPages;
using ModernUINavigationApp1.Services;
using System;
using System.IO.Pipes;
using System.Management;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        private Frame _navigationService;
        private ConnectionService _connectionService;
        private ConnectionOptions _options;
        private ManagementScope _scope;


        public MainMenu (Frame navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            try 
            {
                SetConnectionService();
            }
            catch (Exception e)
            {
                ModernDialog.ShowMessage("Unable to connect CIM\n" + e, "ConnectionService Error", MessageBoxButton.OK);
            }
        }
        public MainMenu(Frame navigationService, string computerName,string userName, string Password)
        {
            InitializeComponent();
            _navigationService = navigationService;
            try
            {
                SetConnectionService(computerName,userName,Password);
            }
            catch (Exception e)
            {
                ModernDialog.ShowMessage("Unable to connect CIM\n" + e, "ConnectionService Error", MessageBoxButton.OK);
            }
        }



        private void btnDiskInfo_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new DiskInfo(_navigationService, _scope, _connectionService));

        }

        private void btnSmart_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new Smart(_navigationService));
        }

        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new Format(_navigationService, _connectionService));
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new CheckDisk(_navigationService));
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new About(_navigationService));
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new Help(_navigationService));
        }

        private void SetConnectionService()
        {
            _connectionService = new ConnectionService();

            _options = new ConnectionOptions();
            _options.Impersonation = _connectionService.SetImpersonationLevel();

            _scope = _connectionService.GetCIMConnection(_options);
            _scope.Connect();
        }

        private void SetConnectionService(string computerName,string userName,string password)
        {

            _connectionService = new ConnectionService();
            _options = new ConnectionOptions();
            _options.Impersonation = System.Management.ImpersonationLevel.Impersonate;

            _options.Username = userName;
            _options.Authority = "NTLMDOMAIN:" + "KOTEG";

            _scope = _connectionService.GetCIMConnection(computerName,_options);
            _scope.Connect();
        }
    }
}
