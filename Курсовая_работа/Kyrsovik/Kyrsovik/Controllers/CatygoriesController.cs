using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kyrsovik.Models;

namespace Kyrsovik.Controllers
{
    public class CatygoriesController : Controller
    {
        private Polina_ZlobinaEntities db = new Polina_ZlobinaEntities();

        // GET: Catygories
        public ActionResult Index()
        {
            return View(db.Catygories.ToList());
        }

        // GET: Catygories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catygories catygories = db.Catygories.Find(id);
            if (catygories == null)
            {
                return HttpNotFound();
            }
            return View(catygories);
        }

        // GET: Catygories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catygories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_category,Name_category")] Catygories catygories)
        {
            if (ModelState.IsValid)
            {
                db.Catygories.Add(catygories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catygories);
        }

        // GET: Catygories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catygories catygories = db.Catygories.Find(id);
            if (catygories == null)
            {
                return HttpNotFound();
            }
            return View(catygories);
        }

        // POST: Catygories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_category,Name_category")] Catygories catygories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catygories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catygories);
        }

        // GET: Catygories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catygories catygories = db.Catygories.Find(id);
            if (catygories == null)
            {
                return HttpNotFound();
            }
            return View(catygories);
        }

        // POST: Catygories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catygories catygories = db.Catygories.Find(id);
            db.Catygories.Remove(catygories);
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
