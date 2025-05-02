using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
     public class clsRole
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _RoleName;

        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        private DateTime _Timestamp;

        public DateTime Timestamp
        {
            get { return _Timestamp; }
            set { _Timestamp = value; }
        }

        public clsRole(
            int _Id,
            string _RoleName,
            DateTime _Timestamp)
        {
            this.Id = _Id;
            this.RoleName = _RoleName;
            this.Timestamp = _Timestamp;
        }
    }
}
