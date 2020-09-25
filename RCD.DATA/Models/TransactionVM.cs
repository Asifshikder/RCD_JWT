using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
   public class TransactionVM
    {
        public bool IsValid { get; set; }
        public double Amount { get; set; }
        public int ReebuxID { get; set; }
        public string Details { get; set; }
        public string Method { get; set; }

    }
}
