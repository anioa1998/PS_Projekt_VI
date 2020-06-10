using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Assets;
using ModernUINavigationApp1.Pages.ActionPages;
using System;
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
        public MainMenu(Frame navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }



        private void btnDiskInfo_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new DiskInfo(_navigationService));

        }

        private void btnSmart_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new Smart(_navigationService));
        }

        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new Format(_navigationService));
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
    }
}
