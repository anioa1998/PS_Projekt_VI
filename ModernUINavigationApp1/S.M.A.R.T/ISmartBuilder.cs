using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using ModernUINavigationApp1.ViewModel;

namespace ModernUINavigationApp1.S.M.A.R.T
{
    public interface ISmartBuilder
    {
        ISmartBuilder SetScope(ManagementScope scope);
        ISmartBuilder SetDriveStorage();
        ISmartBuilder SetViewModel(SMARTViewModel viewModel);
        void Build();
    }
}
