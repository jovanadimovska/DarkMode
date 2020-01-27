using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class WishListItemsByUser
    {
        public List<WishListItem> list { get; set; }
        public WishListItemsByUser()
        {
            list = new List<WishListItem>();
        }
    }
}