using Microsoft.AspNet.Identity;
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
    public class WishListItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult DeleteWishListItem(int? id)
        {
            WishListItem item = db.WishListItems.Find(id);
            db.WishListItems.Remove(item);
            db.SaveChanges();
            return Json(new { Success = true });
        }

        public ActionResult EmptyWishlist()
        {

            foreach (var entity in db.WishListItems)
                db.WishListItems.Remove(entity);
            db.SaveChanges();

            return RedirectToAction("WishList");
        }
        [Authorize]
       
        public ActionResult WishList()
        {
            WishListItemsByUser model = new WishListItemsByUser();
            string userId = User.Identity.GetUserId();
            model.list = db.WishListItems.Include(w=>w.Sunglasses).Where(ci => ci.User == userId).ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult WishList(string SunglassesId)
        {
            int ID = Int32.Parse(SunglassesId);
            string user = User.Identity.GetUserId();
            if (db.WishListItems.Where(c => c.SunglassesId == ID && c.User == user).Count() == 0) {
                WishListItem item = new WishListItem();
                item.User = user;
                item.Sunglasses = db.Sunglasses.Find(ID);
                item.SunglassesId = ID;
                Random rnd = new Random();
                item.Id = rnd.Next();
                db.WishListItems.Add(item);
                db.SaveChanges();
                RedirectToAction("ProductDetail", new { id = SunglassesId });              
            }
                 return Json(new { success = true });
        }
        // GET: WishListItems
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var wishListItems = db.WishListItems.Include(w => w.Sunglasses);
            return View(wishListItems.ToList());
        }

        // GET: WishListItems/Details/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            return View(wishListItem);
        }

        // GET: WishListItems/Create
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.SunglassesId = new SelectList(db.Sunglasses, "Id", "Name");
            return View();
        }

        // POST: WishListItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create([Bind(Include = "Id,User,SunglassesId")] WishListItem wishListItem)
        {
            if (ModelState.IsValid)
            {
                db.WishListItems.Add(wishListItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SunglassesId = new SelectList(db.Sunglasses, "Id", "Name", wishListItem.SunglassesId);
            return View(wishListItem);
        }

        // GET: WishListItems/Edit/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.SunglassesId = new SelectList(db.Sunglasses, "Id", "Name", wishListItem.SunglassesId);
            return View(wishListItem);
        }

        // POST: WishListItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit([Bind(Include = "Id,User,SunglassesId")] WishListItem wishListItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishListItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SunglassesId = new SelectList(db.Sunglasses, "Id", "Name", wishListItem.SunglassesId);
            return View(wishListItem);
        }

        // GET: WishListItems/Delete/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            return View(wishListItem);
        }

        // POST: WishListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult DeleteConfirmed(int id)
        {
            WishListItem wishListItem = db.WishListItems.Find(id);
            db.WishListItems.Remove(wishListItem);
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
