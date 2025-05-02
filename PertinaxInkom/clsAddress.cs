using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsAddress
    {
        private int _Id;

        public int Id 
        { 
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Street_Name;

        public string Street_Name
        {
            get { return _Street_Name; }
            set { _Street_Name = value; }
        }

        private string _House_Number;

        public string House_Number
        {
            get { return _House_Number; }
            set { _House_Number = value; }
        }

        private string? _Bus_number;

        public string? Bus_number
        {
            get { return _Bus_number; }
            set { _Bus_number = value; }
        }

        private string _Zip_Code;

        public string Zip_Code
        {
            get { return _Zip_Code; }
            set { _Zip_Code = value; }
        }

        private string _City;

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        private string _Country;

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        private DateTime _Timestamp;

        public DateTime Timestamp
        {
            get { return _Timestamp; }
            set { _Timestamp = value; }
        }

        public clsAddress(
            int _Id,
            string _Street_Name,
            string _House_Number,
            string _Bus_Number,
            string _Zip_Code,
            string _City,
            string _Country,
            DateTime _Timestamp)
        {
            this.Id = _Id;
            this.Street_Name = _Street_Name;
            this.House_Number = _House_Number;
            this.Bus_number = _Bus_Number;
            this.Zip_Code = _Zip_Code;
            this.City = _City;
            this.Country = _Country;
            this.Timestamp = _Timestamp;
        }
    }
}
