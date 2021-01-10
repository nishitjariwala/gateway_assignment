using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Models
{
    public class Dropdown_Category
    {
        public List<SelectListItem> Category { get; set; }
        public int? Category_Id { get; set; }
    }
}