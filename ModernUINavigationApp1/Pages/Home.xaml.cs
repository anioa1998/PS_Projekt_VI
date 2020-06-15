using System.Windows.Controls;

namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            _NavigationFrame.Navigate(new ConnectionType(_NavigationFrame));
        }


    }
}
