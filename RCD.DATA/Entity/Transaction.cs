using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Entity
{
    public class Transaction : BaseEntity
    {
        public string UserID { get; set; }
        public int? WalletID { get; set; }
        public double? Amount { get; set; }
        public string Method { get; set; }
        public bool IsValid { get; set; }
        public string Details { get; set; }
    }
}
