using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWear.Classes
{
    public class Cart
    {
        public int? Sno { get; set; }
        public int? ProductID { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public double? UnitPrice { get; set; }
        public double? NetPrice { get; set; }
        public double? Quantity { get; set; }
        public int? colorId { get; set; }
        public int? sizeId { get; set; }
        public string colorName { get; set; }
        public string sizeName { get; set; }
    }
}