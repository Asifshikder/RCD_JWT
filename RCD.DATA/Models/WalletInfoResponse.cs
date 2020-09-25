using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
   public class WalletInfoResponse
    {
        public string Message { get; set; }
        public int? WalletID { get; set; }
        public string WalletAddress { get; set; }
        public string UserID { get; set; }
        public double? Balance { get; set; }
        public int? Status { get; set; }
    }
}
