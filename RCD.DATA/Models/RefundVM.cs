using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
    public class RefundVM
    {
        public string UserID { get; set; }
        public int? WalletID { get; set; }
        public double? Amount { get; set; }
        public bool IsGranted { get; set; }
        public string Description { get; set; }
        public int? ReebuxID { get; set; }

    }
}
