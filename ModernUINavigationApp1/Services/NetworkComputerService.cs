using ModernUINavigationApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ModernUINavigationApp1.Services
{
    public class NetworkComputerService
    {
        public void scan(string subnet)
        {
            Ping myPing;
            PingReply reply;
            IPAddress addr;
            IPHostEntry host;
            List<HostAddresses> list = new List<HostAddresses>();

            for (int i = 1; i < 255; i++)
            {
                string subnetn = "." + i.ToString();
                myPing = new Ping();
                reply = myPing.Send(subnet + subnetn, 900);

                if (reply.Status == IPStatus.Success)
                {
                    try
                    {
                        addr = IPAddress.Parse(subnet + subnetn);
                        host = Dns.GetHostEntry(addr);

                        list.Add(new HostAddresses{ IP=subnet + subnetn, Hostname=host.HostName, Status="Up" });
                    }
                    catch { }
                }
            }
           
        }

    }

}
