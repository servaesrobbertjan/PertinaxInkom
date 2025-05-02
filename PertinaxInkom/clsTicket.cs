using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsTicket
    {
        private int _Id;

        public int Id 
        { 
            get { return _Id; } 
            set { _Id = value; } 
        }

        private int _Ticket_Type_Id;

        public int Ticket_Type_Id
        {
            get { return _Ticket_Type_Id; }
            set { _Ticket_Type_Id = value; }
        }

        private int _Buyer_Id;

        public int Buyer_Id
        {
            get { return _Buyer_Id; }
            set { _Buyer_Id = value; }
        }

        private int _User_Id;

        public int User_Id
        {
            get { return _User_Id; }
            set { _User_Id = value; }
        }

        private string _Ticket_Uuid;

        public string Ticket_Uuid
        {
            get { return _Ticket_Uuid; }
            set { _Ticket_Uuid = value; }
        }

        private DateTime _Order_Date;

        public DateTime Order_Date
        {
            get { return _Order_Date; }
            set { _Order_Date = value; }
        }

        public clsTicket(
            int _Id,
            int _Ticket_Type_Id,
            int _Buyer_Id,
            int _User_Id,
            string _Ticket_Uuid,
            DateTime _Order_Date)
        { 
            this.Id = _Id;
            this.Ticket_Type_Id = _Ticket_Type_Id;
            this.Buyer_Id = _Buyer_Id;
            this._User_Id = _User_Id;
            this.Ticket_Uuid = _Ticket_Uuid;
            this.Order_Date = _Order_Date;
        }
    }
}
