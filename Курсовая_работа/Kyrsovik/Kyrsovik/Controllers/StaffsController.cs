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
    public class StaffsController : Controller
    {
        private Polina_ZlobinaEntities db = new Polina_ZlobinaEntities();

        // GET: Staffs
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.FIOSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "name_desc";
            ViewBag.PositionSortParm = sortOrder == "Position" ? "position_desc" : "Position";
            var staff = from s in db.Staff select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                staff = staff.Where(s => s.FIO.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    staff = staff.OrderBy(s => s.FIO);
                    break;
                case "name_desc":
                    staff = staff.OrderByDescending(s => s.FIO);
                    break;
                case "Position":
                    staff = staff.OrderBy(s => s.ID_position);
                    break;
                case "position_desc":
                    staff = staff.OrderByDescending(s => s.ID_position);
                    break;
                default:
                    staff = staff.OrderBy(s => s.ID_staff);
                    break;
            }

            return View(staff.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            ViewBag.ID_position = new SelectList(db.Position, "ID_position", "Name_position");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_staff,ID_position,FIO,Phone,Photo,E_mail,Password,Adress,Passpost,Requisites")] Staff staff, HttpPostedFileBase upload)
        {
            if(ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        staff.Photo = reader.ReadBytes(upload.ContentLength);
                    }
                }
                db.Staff.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_position = new SelectList(db.Position, "ID_position", "Name_position", staff.ID_position);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_position = new SelectList(db.Position, "ID_position", "Name_position", staff.ID_position);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_staff,ID_position,FIO,Phone,Photo,E_mail,Password,Adress,Passpost,Requisites")] Staff staff, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(staff).State = EntityState.Modified;
                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            staff.Photo = reader.ReadBytes(upload.ContentLength);
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(staff).Property(m => m.Photo).IsModified = false;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                ViewBag.ID_position = new SelectList(db.Position, "ID_position", "Name_position", staff.ID_position);
                return View(staff);
            }
            catch (Exception e) { return View(staff); }
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staff.Find(id);
            db.Staff.Remove(staff);
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
