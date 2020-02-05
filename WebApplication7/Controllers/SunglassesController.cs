using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class SunglassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ProductDetail()
        {
            //da se zeme real id
            ProductDetail model = new ProductDetail();
            model.listSimilar = db.Sunglasses.ToList();
            int id = int.Parse(this.RouteData.Values["id"].ToString());
            model.sunglasses = db.Sunglasses.Find(id);
            return View(model);
        }
        public ActionResult HomePage()
        {
            HomePageViewModel model = new HomePageViewModel();
            model.sunglasses = db.Sunglasses.ToList();
            model.sunglassesForSlider.Add(model.sunglasses[0]);
            model.sunglassesForSlider.Add(model.sunglasses[1]);
            model.sunglassesForSlider.Add(model.sunglasses[2]);
            return View(model);
        }
        public ActionResult Category()
        {
            CategoryViewModel model = new CategoryViewModel();
            model.category = Request.Params["category"];
            model.sunglasses = db.Sunglasses.Include(s=>s.Brand).Where(c => c.category.ToString().Equals(model.category)).ToList();
            model.brands = db.Brands.ToList();
            return View(model);
        }
        // GET: Sunglasses1
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var sunglasses = db.Sunglasses.Include(s => s.Brand);
            return View(sunglasses.ToList());
        }

        // GET: Sunglasses1/Details/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sunglasses sunglasses = db.Sunglasses.Find(id);
            if (sunglasses == null)
            {
                return HttpNotFound();
            }
            return View(sunglasses);
        }

        // GET: Sunglasses1/Create
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            return View();
        }

        // POST: Sunglasses1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create([Bind(Include = "Id,Price,Name,imgUrl,BrandId,sale,category,rating")] Sunglasses sunglasses)
        {
            if (ModelState.IsValid)
            {
                db.Sunglasses.Add(sunglasses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", sunglasses.BrandId);
            return View(sunglasses);
        }

        // GET: Sunglasses1/Edit/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sunglasses sunglasses = db.Sunglasses.Find(id);
            if (sunglasses == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", sunglasses.BrandId);
            return View(sunglasses);
        }

        // POST: Sunglasses1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit([Bind(Include = "Id,Price,Name,imgUrl,BrandId,sale,category,rating")] Sunglasses sunglasses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sunglasses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", sunglasses.BrandId);
            return View(sunglasses);
        }

        // GET: Sunglasses1/Delete/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sunglasses sunglasses = db.Sunglasses.Find(id);
            if (sunglasses == null)
            {
                return HttpNotFound();
            }
            return View(sunglasses);
        }

        // POST: Sunglasses1/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sunglasses sunglasses = db.Sunglasses.Find(id);
            db.Sunglasses.Remove(sunglasses);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
