using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsWallet
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private decimal _Balans;

        public decimal Balans
        {
            get { return _Balans; }
            set { _Balans = value; }
        }

        private int _Pincode;

        public int Pincode
        {
            get { return _Pincode; }
            set { _Pincode = value; }
        }

        public clsWallet(
            int _Id,
            decimal _Balans,
            int _Pincode)
        {
            this.Id = _Id;
            this.Balans = _Balans;
            this.Pincode = _Pincode;
        }
    }
}
