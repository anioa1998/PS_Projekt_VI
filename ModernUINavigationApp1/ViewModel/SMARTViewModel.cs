using ModernUINavigationApp1.Model;
using ModernUINavigationApp1.S.M.A.R.T;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ModernUINavigationApp1.ViewModel
{
    public class SMARTViewModel
    {
        private Dictionary<int, DriveData> _allDrivesData;
        private Dictionary<string, List<SMARTModel>> _viewData;
        private string[] _serialNumbers;
        private SMARTModel[] _smartData;
        public string[] SerialNumbers
        {
            get { return _serialNumbers; }
        }
        public SMARTModel[] SmartData
        {
            get { return _smartData; }
        }

        public void SetInformation(Dictionary<int, DriveData> allDrivesData)
        {
            _allDrivesData = allDrivesData;
            List<string> serialNumbers = new List<string>();
            Dictionary<string, List<SMARTModel>> fullListToView = new Dictionary<string, List<SMARTModel>>();


            string[] boolAnswerData = { "", "OK" };

            int select;

            foreach (var drive in allDrivesData)
            {
                serialNumbers.Add(drive.Value.Serial);

                List<SMARTModel> temporaryDataList = new List<SMARTModel>();

                foreach (var attr in drive.Value.Attributes)
                {
                    if (attr.Value.CheckIfHasData)
                    {
                        select = attr.Value.Status ? 1 : 0;

                        SMARTModel tempModel = new SMARTModel()
                        {
                            Attribute = attr.Value.Attribute,
                            Current = attr.Value.Current,
                            Worst = attr.Value.Worst, 
                            Threshold = attr.Value.Threshold,
                            Data = attr.Value.Data,
                            Status = boolAnswerData[select]
                        };
                        temporaryDataList.Add(tempModel);
                    }
                }
                fullListToView.Add(drive.Value.Serial, temporaryDataList);
            }
            _serialNumbers = serialNumbers.ToArray();
            _viewData = fullListToView;
        }

        public void SetData(string serialNumber)
        {
            List<SMARTModel> smartObjects = new List<SMARTModel>();
            smartObjects = _viewData.Where(x => x.Key == serialNumber)
                                               .Select(x => x.Value)
                                               .Single();
            _smartData = smartObjects.ToArray();
        }
    }
}
