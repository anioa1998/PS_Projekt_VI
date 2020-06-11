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

    }
}
