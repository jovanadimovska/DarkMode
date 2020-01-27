using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    
    public class Sunglasses
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string imgUrl { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public bool sale { get; set; }
        public Category category { get; set; }
        public int rating { get; set; }

    }
}