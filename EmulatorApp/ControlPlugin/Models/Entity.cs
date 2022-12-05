using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace Emulator.ControlPlugin.Models
{
    public class Entity : Model
    {
        private string _id;
        private string _dataName;
        string _description;
        private string _dataValue;
        private string _orderNumber;

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string DataName
        {
            get { return _dataName; }
            set { SetProperty(ref _dataName, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string DataValue
        {
            get { return _dataValue; }
            set { SetProperty(ref _dataValue, value); }
        }

        public string OrderNumber
        {
            get { return _orderNumber; }
            set { SetProperty(ref _orderNumber, value); }
        }
    }
}
