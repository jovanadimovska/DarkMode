using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class CategoryViewModel
    {
        public List<Sunglasses> sunglasses { get; set; }
        public string category;
        public List<Brand> brands { get; set; }
        public CategoryViewModel() {
            sunglasses = new List<Sunglasses>();
            brands = new List<Brand>();
        }
    }
}