using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
    public class BuyProductVM
    {
        public int ProductID { get; set; }
        public string Details { get; set; }
        public int? Amount { get; set; }
    }
}
