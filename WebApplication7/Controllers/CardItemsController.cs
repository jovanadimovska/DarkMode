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



    public class CardItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        public ActionResult ShoppingCard()
        {
            ShoppingCardList model = new ShoppingCardList();
            string userId = User.Identity.GetUserId();
            model.list = db.CardItems.Where(ci => ci.User == userId).ToList();
            
            foreach (CardItem c in model.list)
            {
                c.Sunglasses = db.Sunglasses.Find(c.SunglassesId);
                model.total = model.total + (c.Sunglasses.Price * c.Quantity);
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]

        public ActionResult ShoppingCard(string SunglassesId)
        {
            int ID = Int32.Parse(SunglassesId);
            string user = User.Identity.GetUserId();
            if (db.CardItems.Where(c => c.SunglassesId == ID&&c.User == user).Count() == 0)
            {
                
                CardItem cardItem = new CardItem();
                cardItem.User = user;
                cardItem.Sunglasses = db.Sunglasses.Find(ID);
                cardItem.Quantity = 1;
                cardItem.SunglassesId = ID;
                Random rnd = new Random();
                cardItem.Id = rnd.Next();
                db.CardItems.Add(cardItem);
                db.SaveChanges();
                List<CardItem> listaaa = db.CardItems.ToList();
                RedirectToAction("ProductDetail", new { id = SunglassesId });
                return Json(new { success = true });
            }
            else {
                db.CardItems.Where(c => c.SunglassesId == ID).FirstOrDefault().Quantity++;
                db.SaveChanges();
                return Json(new { success = true });
            }

        }
        // GET: CardItems
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var cardItems = db.CardItems.Include(c => c.Sunglasses);
            return View(cardItems.ToList());
        }

        // GET: CardItems/Details/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardItem cardItem = db.CardItems.Find(id);
            if (cardItem == null)
            {
                return HttpNotFound();
            }
            return View(cardItem);
        }

        // GET: CardItems/Create
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.SunglassesId = new SelectList(db.Sunglasses, "Id", "Name");
            return View();
        }

        // POST: CardItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create([Bind(Include = "Id,User,Quantity,SunglassesId")] CardItem cardItem)
        {
            if (ModelState.IsValid)
            {
                db.CardItems.Add(cardItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SunglassesId = new SelectList(db.Sunglasses, "Id", "Name", cardItem.SunglassesId);
            return View(cardItem);
        }

        // GET: CardItems/Edit/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardItem cardItem = db.CardItems.Find(id);
            if (cardItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.SunglassesId = new SelectList(db.Sunglasses, "Id", "Name", cardItem.SunglassesId);
            return View(cardItem);
        }

        // POST: CardItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit([Bind(Include = "Id,User,Quantity,SunglassesId")] CardItem cardItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cardItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SunglassesId = new SelectList(db.Sunglasses, "Id", "Name", cardItem.SunglassesId);
            return View(cardItem);
        }

        // GET: CardItems/Delete/5
        [Authorize]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardItem cardItem = db.CardItems.Find(id);
            if (cardItem == null)
            {
                return HttpNotFound();
            }
            return View(cardItem);
        }
        [HttpPost]
        [Authorize]
        public ActionResult DeleteCardItem(int? id)
        {
            CardItem cardItem = db.CardItems.Find(id);
            db.CardItems.Remove(cardItem);
            db.SaveChanges();
            return Json(new { Success = true});
        }
        [HttpPost]
        [Authorize]
        public ActionResult DownCardItem(int? id)
        {
            db.CardItems.Where(c => c.Id == id).FirstOrDefault().Quantity++;
            db.SaveChanges();
            return Json(new { Success = true });
        }
        [HttpPost]
        [Authorize]
        public ActionResult UpCardItem(int? id)
        {
            if (db.CardItems.Where(c => c.Id == id).FirstOrDefault().Quantity > 0)
            {
                db.CardItems.Where(c => c.Id == id).FirstOrDefault().Quantity--;
                db.SaveChanges();
            }
            return Json(new { Success = true });
        }
        [HttpPost]
        [Authorize]
        public ActionResult BuyProducts()
        {

            foreach (var entity in db.CardItems)
                db.CardItems.Remove(entity);
            db.SaveChanges();

            return RedirectToAction("ShoppingCard");
        }
        // POST: CardItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int? id)
        {
            CardItem cardItem = db.CardItems.Find(id);
            db.CardItems.Remove(cardItem);
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
