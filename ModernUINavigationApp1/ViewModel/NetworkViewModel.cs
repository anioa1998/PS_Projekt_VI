using ModernUINavigationApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUINavigationApp1.ViewModel
{
    public class NetworkViewModel
    {
        public string[] ConvertIPData (List<HostAddresses> hostAddresses)
        {
            List<string> ipInString = new List<string>();
            foreach(var host in hostAddresses)
            {
                string oneRecord = $"IP: {host.IP}; Name: {host.Hostname}; Status: {host.Status};";
                ipInString.Add(oneRecord);
            }
            return ipInString.ToArray();
        }
    }
}
