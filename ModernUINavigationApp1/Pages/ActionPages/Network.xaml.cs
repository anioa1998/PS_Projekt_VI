using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Model;
using ModernUINavigationApp1.Services;
using ModernUINavigationApp1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ModernUINavigationApp1.Pages.ActionPages
{
    /// <summary>
    /// Interaction logic for Network.xaml
    /// </summary>
    public partial class Network : UserControl
    {
        private Frame _navigationService { get; set; }
        private NetworkComputerService _networkService { get; set; }
        private NetworkViewModel _dataContext { get; set; }
        public Network(Frame navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _networkService = new NetworkComputerService();
            _dataContext = new NetworkViewModel();
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
            try
            {

                string ipOctets = txtIP.Text;
                int ipOctetsLength = ipOctets.Length;
                int dotCoutner = 0;
                if (ipOctetsLength < 5 || ipOctetsLength > 11)
                {
                    throw CheckException();
                }
                foreach (var letter in ipOctets)
                {
                    if ('.' == letter)
                        dotCoutner++;
                }
                if (dotCoutner != 2)
                {
                    throw CheckException();
                }
                if (0 != Regex.Matches(ipOctets, @"[a-zA-Z]").Count)
                {
                    throw CheckException();
                }

                if(hasSpecialChar(ipOctets))
                {
                    throw CheckException();
                }

                List<HostAddresses> scanResult = _networkService.scan(ipOctets);
                if (null != scanResult)
                {
                    lstViewAddress.ItemsSource = _dataContext.ConvertIPData(scanResult);
                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Try Again!", MessageBoxButton.OK);
            }
        }
        private Exception CheckException()
        {
            return new Exception("Wrong 3 octet definition!\nYour 3 octets should apply schema XXX.XXX.XXX eg. 192.168.1");
        }

        private bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
    }
}
