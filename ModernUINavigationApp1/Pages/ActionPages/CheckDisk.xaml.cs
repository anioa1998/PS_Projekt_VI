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

namespace ModernUINavigationApp1.Pages.ActionPages
{
    /// <summary>
    /// Interaction logic for CheckDisk.xaml
    /// </summary>
    public partial class CheckDisk : UserControl
    {
        private Frame _navigationService;

        public CheckDisk(Frame navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
