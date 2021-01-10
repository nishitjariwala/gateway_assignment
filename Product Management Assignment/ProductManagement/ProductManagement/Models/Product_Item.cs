using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Product_Item
    {
        
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Short_Desc { get; set; }
        public string Long_Desc { get; set; }
        public string Small_Image_path { get; set; }
        public string Lage_Image_path { get; set; }
        public string Category { get; set; }
    }
}