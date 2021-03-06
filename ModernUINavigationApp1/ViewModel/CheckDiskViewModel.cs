﻿using ModernUINavigationApp1.Services;
using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;

namespace ModernUINavigationApp1.ViewModel
{

    public class CheckDiskViewModel
    {
        [DllImport(@"Librares/Win32_CheckDisk_CPP.dll", EntryPoint = "chkdsk", CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckDisk([MarshalAs(UnmanagedType.BStr)] string letter);
        private string[] _logicalDiskNames;
        private ConnectionService _connectionService { get; set; }
        private ManagementObjectCollection _queryCollection { get; set; }

        public string[] statusType = new string[]
        {
            "Success - Chkdsk completed",
            "Success - Locked and chkdsk scheduled on reboot",
            "Failure - Unknown file system",
            "Failure - Unknown error",
            "Failure - Unsupported File System"
         };

        public CheckDiskViewModel(ConnectionService connectionService)
        {
            _connectionService = connectionService;
            _logicalDiskNames = GetLogicalDiskNames();
        }


        public string[] LogicalDiskNames
        {
            get { return this._logicalDiskNames; }
        }

        private string[] GetLogicalDiskNames()
        {
            List<string> logicalDiskNames = new List<string>();

            try
            {
                _queryCollection = _connectionService.GetQueryCollectionFromWin32Class("Win32_LogicalDisk");


                foreach (ManagementObject logicalDiskData in _queryCollection)
                {
                    string logicalDiskName = logicalDiskData["Name"].ToString();
                    logicalDiskNames.Add(logicalDiskName);
                }
                return logicalDiskNames.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine("CheckDiskInformation Error: " + e.Message);
                return logicalDiskNames.ToArray();
            }
        }

        public string ReturnStatus(string diskLetter)
        {
            string convertedLetter = $"\"{diskLetter}\"";
            int result = 0;
            string query2 = "Win32_LogicalDisk.DeviceID=" + convertedLetter;
            var thread = new Thread(() =>
            {
                result = CheckDisk(query2);
            });
            thread.Start();
            thread.Join();
            return statusType[result];
        }
    }
}
