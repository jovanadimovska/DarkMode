using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class HomePageViewModel
    {
        public List<Sunglasses> sunglasses;
        public List<Sunglasses> sunglassesForSlider;
        public HomePageViewModel()
        {
            sunglasses = new List<Sunglasses>();
            sunglassesForSlider = new List<Sunglasses>();
        }
    }
}