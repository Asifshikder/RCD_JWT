using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Entity
{
    public class Reebux : BaseEntity
    {
        public string Title { get; set; }
        public double? Price { get; set; }
        public double? Balance { get; set; }
        public int Amount { get; set; }
        public string ImageUrl { get; set; }

    }
}
