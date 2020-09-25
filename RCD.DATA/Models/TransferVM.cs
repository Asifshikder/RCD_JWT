using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
    public class TransferVM
    {
    
        public string RecieverWallet { get; set; }
        public string SenderWallet { get; set; }
        public double? Amount { get; set; }
        public string Description { get; set; }
        public bool? IsGift { get; set; }
    }
}
