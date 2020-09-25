using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Entity
{
    public class Refund : BaseEntity
    {
        public string UserID { get; set; }
        public int? WalletID { get; set; }
        public double? Amount { get; set; }
        public bool IsGranted { get; set; }
        public string  Description { get; set; }
        public int? ReebuxID { get; set; }
    }
}
