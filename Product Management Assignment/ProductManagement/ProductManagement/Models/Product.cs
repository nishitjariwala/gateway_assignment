using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Product
    {

        public int Product_Id { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Enter Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is Required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Short Description is Required")]
        [StringLength(50, MinimumLength = 10)]
        [DisplayName("Short Description")]
        public string Short_Desc { get; set; }

        [Required(ErrorMessage = "Long Description is Required")]
        [DisplayName("Long Description")]
        public string Long_Desc { get; set; }
        [Required]
        public string Small_Image_path { get; set; }
        [Required]
        public string Lage_Image_path { get; set; }
        public HttpPostedFileBase Small_Image { get; set; }
        public HttpPostedFileBase Lage_Image { get; set; }


    }
}