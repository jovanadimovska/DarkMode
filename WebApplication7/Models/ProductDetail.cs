using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class ProductDetail
    {
        public Sunglasses sunglasses { get; set; }
        public List<Sunglasses> listSimilar { get; set; }
        public ProductDetail() {
            listSimilar = new List<Sunglasses>();
            sunglasses = new Sunglasses();
        }
    }
}