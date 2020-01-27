using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class CardItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string User { get; set; }

        public int Quantity { get; set; }
        public int SunglassesId{get;set;}
        public  Sunglasses Sunglasses { get; set; }
       
    }
}