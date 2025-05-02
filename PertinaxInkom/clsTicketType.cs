using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsTicketType
    {
        private int _Id;

        public int Id 
        { 
            get { return _Id; } 
            set { _Id = value; } 
        }

        private string _Name;

        public string Name 
        { 
            get { return _Name; } 
            set { _Name = value; }
        }

        private decimal _Price;

        public decimal Price 
        {
            get { return _Price; }
            set { _Price = value; } 
        }

        private int? _Amount;

        public int? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private DateTime _Timestamp;

        public DateTime Timestamp
        {
            get { return _Timestamp; }
            set { _Timestamp = value; }
        }

        public clsTicketType(
            int _Id,
            string _Name,
            decimal _Price,
            int? _Amount,
            DateTime _Timestamp)
        {
            this.Id = _Id;
            this.Name = _Name;
            this.Price = _Price;
            this.Amount = _Amount;
            this.Timestamp = _Timestamp;
        }
    }
}
