using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Entity
{
    public class Order : BaseEntity
    {
        public int? ProductID { get; set; }
        public string UserID { get; set; }
        public double? TotalPrice { get; set; }
    }
}
