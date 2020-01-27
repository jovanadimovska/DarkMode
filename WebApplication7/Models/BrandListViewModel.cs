using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class BrandListViewModel
    {
        public List<Sunglasses> listSunglasses { get; set; }
        public string brand { get; set; }
        public BrandListViewModel() {
            this.listSunglasses = new List<Sunglasses>();
        }
    }
}