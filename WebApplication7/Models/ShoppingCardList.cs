using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class ShoppingCardList
    {
        public List<CardItem> list { get; set; }
        public int total { get; set; }
        public ShoppingCardList() {
            list = new List<CardItem>();
            this.total = 0;
        }
    }
}