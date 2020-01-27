using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class WishListItem
    {
        public int Id { get; set; }

        public string User { get; set; }

        public int SunglassesId { get; set; }
        public Sunglasses Sunglasses { get; set; }

        public WishListItem() {

        }
    }
}