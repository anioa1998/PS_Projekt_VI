using FirstFloor.ModernUI.Windows.Controls;
using ModernUINavigationApp1.Services;
using ModernUINavigationApp1.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
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
    /// Interaction logic for Format.xaml
    /// </summary>
    public partial class Format : UserControl
    {
        private Frame _navigationService;
        private FormatViewModel _dataContext;

        public Format(Frame navigationService, ConnectionService connectionService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _dataContext = new FormatViewModel(connectionService); //to use methods in DiskInfoViewModel outside
            DataContext = _dataContext;
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

        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = ModernDialog.ShowMessage("Are sure you want to format this partition? \nThis operation is unrevertable so think twice or even triple.", "Format Warning", MessageBoxButton.YesNo);
            txtBlockEnd.Visibility = Visibility.Hidden;
            if (result == MessageBoxResult.Yes)
            {
                string driveLetter = (string)cmbBoxLetter.SelectedItem;
                string fileSystem = (string)cmbBoxSystem.SelectedItem;
                string label = txtName.Text;
                bool quickFormat = true;
                int clusterSize = 8192;
                bool enableCompression = false;
                try
                {
                    
                    if (string.IsNullOrEmpty(driveLetter) || string.IsNullOrEmpty(fileSystem) || string.IsNullOrEmpty(label))
                    {
                        EmptyException ex = new EmptyException();
                        throw ex;
                    }
                    var files = Directory.GetFiles(driveLetter);
                    var directories = Directory.GetDirectories(driveLetter);

                    foreach (var item in files)
                    {
                        try
                        {
                            File.Delete(item);
                        }
                        catch (UnauthorizedAccessException) { }
                        catch (IOException) { }
                    }

                    foreach (var item in directories)
                    {
                        try
                        {
                            Directory.Delete(item);
                        }
                        catch (UnauthorizedAccessException) { }
                        catch (IOException) { }
                    }
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"select * from Win32_Volume WHERE DriveLetter = '" + driveLetter + "'");
                    foreach (ManagementObject vi in searcher.Get())
                    {
                        try
                        {
                            var completed = false;
                            var watcher = new ManagementOperationObserver();

                            watcher.Completed += (sender1, args) =>
                            {
                                Console.WriteLine("INFO Errors: " + args.Status);
                                Console.WriteLine("\n");


                                completed = true;
                            };
                            

                            vi.InvokeMethod(watcher, "Format", new object[] { fileSystem, quickFormat, clusterSize, label, enableCompression });

                            while (!completed) { System.Threading.Thread.Sleep(1000); }

                        }
                        catch
                        {
                            ModernDialog.ShowMessage("Something went wrong with partition format. Our apologize.", "Error!", MessageBoxButton.OK);
                        }
                    }

                    txtBlockEnd.Visibility = Visibility.Visible;
                }
                catch (EmptyException ex)
                {
                    ModernDialog.ShowMessage("One of the selected values is invalid. Non of the values can be left empty. \nTry again.", "Error!", MessageBoxButton.OK);
                }
               
            }
        }
        class EmptyException : Exception
        {
            public EmptyException()
            {
            }
        }
    } 
}
