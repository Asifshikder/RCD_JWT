using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.DATA.Models
{
    public class ProductCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public string Avatar { get; set; }
        public double? Balance { get; set; }
        public string ImageUrl { get; set; }
    }
}
