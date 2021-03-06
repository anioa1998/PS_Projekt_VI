﻿using FirstFloor.ModernUI.Windows.Controls;
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

namespace ModernUINavigationApp1.Pages
{
    /// <summary>
    /// Interaction logic for DiskInfo.xaml
    /// </summary>
    public partial class DiskInfo : UserControl
    {
        private Frame _navigationService;
        public DiskInfo(Frame navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            this.DataContext = new DiskInfoViewModel();
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
    }
}
