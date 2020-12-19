using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _4_task.Models;

namespace _4_task.Controllers
{
    public class ManagersController : Controller
    {
        private Travel_CompanyEntities db = new Travel_CompanyEntities();

        // GET: Managers
        public ActionResult Index()
        {
            return View(db.Managers.ToList());
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Managers managers = db.Managers.Find(id);
            if (managers == null)
            {
                return HttpNotFound();
            }
            return View(managers);
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_manager,FIO_manager")] Managers managers)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(managers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(managers);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Managers managers = db.Managers.Find(id);
            if (managers == null)
            {
                return HttpNotFound();
            }
            return View(managers);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_manager,FIO_manager")] Managers managers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(managers);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Managers managers = db.Managers.Find(id);
            if (managers == null)
            {
                return HttpNotFound();
            }
            return View(managers);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Managers managers = db.Managers.Find(id);
            db.Managers.Remove(managers);
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
