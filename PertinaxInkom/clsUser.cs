using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
     public class clsUser
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int? _Address_Id;

        public int? Address_Id
        {
            get { return _Address_Id; }
            set { _Address_Id = value; }
        }

        private int? _Wallet_Id;

        public int? Wallet_Id
        {
            get { return _Wallet_Id; }
            set { _Wallet_Id = value; }
        }

        private string _Nick_Name;

        public string Nick_Name
        {
            get { return _Nick_Name; }
            set { _Nick_Name = value; }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string? _First_Name;

        public string? First_Name
        {
            get { return _First_Name; }
            set { _First_Name = value; }
        }

        private string? _Last_Name;

        public string? Last_Name
        {
            get { return _Last_Name; }
            set { _Last_Name = value; }
        }

        private string? _Email;

        public string? Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        
        private string? _Uuid;

        public string? Uuid
        {
            get { return _Uuid; }
            set { _Uuid = value; }
        }

        private DateTime? _Birth_Date;

        public DateTime? Birth_Date
        {
            get { return _Birth_Date; }
            set { _Birth_Date = value; }
        }

        private DateTime _Timestamp;

        public DateTime Timestamp
        {
            get { return Timestamp; }
            set { _Timestamp = value; }
        }

        public clsUser(
            int _Id,
            int _Address_Id,
            int _Wallet_Id,
            string _Nick_Name,
            string _Password,
            string _First_Name,
            string _Last_Name,
            string _Email,
            string? _Uuid,
            DateTime? _Birth_Date,
            DateTime _Timestamp)
        {
            this.Id = _Id;
            this.Address_Id = _Address_Id;
            this.Wallet_Id = _Wallet_Id;
            this._Nick_Name = _Nick_Name;
            this.Password = _Password;
            this.First_Name = _First_Name;
            this.Last_Name = _Last_Name;
            this.Email = _Email;
            this.Uuid = _Uuid;
            this.Birth_Date = _Birth_Date;
            this.Timestamp = _Timestamp;
        }
    }
}
