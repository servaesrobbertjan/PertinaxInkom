using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsUserRole
    {
        private int _Id;

        public int Id 
        { 
            get { return _Id; }
            set { _Id = value; }
        }

        private int _UserId;

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private int _RoleId;

        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        private DateTime _CreatedAt;

        public DateTime CreatedAt
        {
            get { return CreatedAt; }
            set { _CreatedAt = value; }
        }

        private DateTime? _UpdatedAt;

        public DateTime? UpdateAt
        {
            get { return UpdateAt; }
            set { _UpdatedAt = value; }
        }

        public clsUserRole
            (
            int _Id,
            int _UserId,
            int _RoleId,
            DateTime _CreatedAt,
            DateTime? _UpdatedAt)
        {
            this.Id = _Id;
            this.UserId = _UserId;
            this.RoleId = _RoleId;
            this.CreatedAt = _CreatedAt;
            this.UpdateAt = _UpdatedAt;
        }
    }
}
