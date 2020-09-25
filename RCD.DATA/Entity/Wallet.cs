using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Entity
{
    public class Wallet : BaseEntity
    {
        public string Address { get; set; }
        public string UserID { get; set; }
        public double? Balance { get; set; }

    }
}
