using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CategoryNewProject.Models
{
    public class ProductPageViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set;}
        public string ProductDescription { get; set;}
        public int CategoryId { get; set;}
    }
}