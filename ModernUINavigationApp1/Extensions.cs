using ModernUINavigationApp1.Pages.ActionPages.DiskInfoPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUINavigationApp1
{
    public static class Extensions
    {
        public static string ToGB(this string value)
        {
            return ConvertToGB(value);
        }
        public static string ConvertToGB(string value)
        {
            long intValue = Convert.ToInt64(value);
            double doubleValue = intValue / 1000000000;
            string gbValue = doubleValue.ToString() + " GB";
            return gbValue;
        }
    }
}
