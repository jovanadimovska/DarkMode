using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class BrandsListViewModel
    {
        public List<Brand> brands { get; set; }
        public BrandsListViewModel() {
            brands = new List<Brand>();
        }
    }
}