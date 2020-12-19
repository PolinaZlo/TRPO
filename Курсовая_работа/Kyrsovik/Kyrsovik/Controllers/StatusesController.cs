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
    public class StatusesController : Controller
    {
        private Polina_ZlobinaEntities db = new Polina_ZlobinaEntities();

        // GET: Statuses
        public ActionResult Index()
        {
            return View(db.Statuses.ToList());
        }

        // GET: Statuses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statuses statuses = db.Statuses.Find(id);
            if (statuses == null)
            {
                return HttpNotFound();
            }
            return View(statuses);
        }

        // GET: Statuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_status,Name_status")] Statuses statuses)
        {
            if (ModelState.IsValid)
            {
                db.Statuses.Add(statuses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statuses);
        }

        // GET: Statuses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statuses statuses = db.Statuses.Find(id);
            if (statuses == null)
            {
                return HttpNotFound();
            }
            return View(statuses);
        }

        // POST: Statuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_status,Name_status")] Statuses statuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statuses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statuses);
        }

        // GET: Statuses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statuses statuses = db.Statuses.Find(id);
            if (statuses == null)
            {
                return HttpNotFound();
            }
            return View(statuses);
        }

        // POST: Statuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statuses statuses = db.Statuses.Find(id);
            db.Statuses.Remove(statuses);
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
