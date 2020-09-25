using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Entity
{
    public class Transfer: BaseEntity
    {
        public string RecieverID { get; set; }
        public string SenderID { get; set; }
        public string RecieverWalletAddress { get; set; }
        public string SenderWalletAddress { get; set; }
        public double? Amount { get; set; }
        public string Description { get; set; }
        public bool? IsGift { get; set; }

    }
}
