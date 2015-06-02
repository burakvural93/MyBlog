using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcBurakBlog.Models;
using PagedList;
namespace MvcBurakBlog.Controllers
{
    [ValidateInput(false)]
    public class MakalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Makales
        public ActionResult Index()
        {
            return View(db.Makales.ToList());
        }
        [HttpGet ]
        public ActionResult Index(string searchString,int sayfa = 1)  
        {
            var articles = from m in db.Makales
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.Baslik.Contains(searchString));
               
            } 
            return View(db.Makales.OrderBy(x => x.Baslik).ToPagedList(sayfa, 10));
        }

       
      


        // GET: Makales/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = db.Makales.Find(id);
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }

        // GET: Makales/Create
        
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Makales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MakaleId,Baslik,Icerik,Tarih,Yazar")] Makale makale)
        {
            if (ModelState.IsValid)
            {
                makale.Tarih = DateTime.Now;
                makale.Yazar = User.Identity.Name;
                db.Makales.Add(makale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(makale);
        }

        // GET: Makales/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = db.Makales.Find(id);
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }

        // POST: Makales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "MakaleId,Baslik,Icerik,Tarih,Yazar")] Makale makale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(makale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(makale);
        }

        // GET: Makales/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = db.Makales.Find(id);
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }

        // POST: Makales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Makale makale = db.Makales.Find(id);
            db.Makales.Remove(makale);
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
