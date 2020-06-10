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

namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for Smart.xaml
    /// </summary>
    public partial class Smart : UserControl
    {
        private Frame _navigationService;

        public Smart(Frame navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

    }
}
