using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    class clsBlockedUuids
    {
        private int _Id;

        public int Id
        { 
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Uuid;

        public string Uuid
        {
            get { return _Uuid; }
            set { _Uuid = value; }
        }

        public clsBlockedUuids(
            int _Id,
            string _Uuid)
        {
            this.Id = _Id;
            this.Uuid = _Uuid;
        }
    }
}
