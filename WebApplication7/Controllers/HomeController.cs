using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return RedirectToAction("HomePage", "Sunglasses");
        }
        [ChildActionOnly]
        public ActionResult getBrands()
        {
            BrandsListViewModel model = new BrandsListViewModel();
            model.brands = db.Brands.ToList();
            return PartialView("_navbar", model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}