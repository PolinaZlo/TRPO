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
    public class SalesController : Controller
    {
        private Polina_ZlobinaEntities db = new Polina_ZlobinaEntities();

        // GET: Sales
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "name_desc";
            ViewBag.DataSortParm = sortOrder == "Data" ? "Data_desc" : "Data";
            ViewBag.StaffSortParm = sortOrder == "Staff" ? "staff_desc" : "Staff";
            var sales = from s in db.Sales select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                sales = sales.Where(s => s.Products.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Name":
                    sales = sales.OrderBy(s => s.Products.Name);
                    break;
                case "name_desc":
                    sales = sales.OrderByDescending(s => s.Products.Name);
                    break;
                case "Data":
                    sales = sales.OrderBy(s => s.Data_sale);
                    break;
                case "Data_desc":
                    sales = sales.OrderByDescending(s => s.Data_sale);
                    break;
                case "Staff":
                    sales = sales.OrderBy(s => s.ID_staff);
                    break;
                case "staff_desc":
                    sales = sales.OrderByDescending(s => s.ID_staff);
                    break;
                default:
                    sales = sales.OrderBy(s => s.ID_sale);
                    break;
            }
            return View(sales.ToList());
        }



















        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.ID_product = new SelectList(db.Products, "ID_product", "Name");
            ViewBag.ID_staff = new SelectList(db.Staff, "ID_staff", "FIO");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_sale,ID_product,ID_staff,Data_sale")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_product = new SelectList(db.Products, "ID_product", "Name", sales.ID_product);
            ViewBag.ID_staff = new SelectList(db.Staff, "ID_staff", "FIO", sales.ID_staff);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_product = new SelectList(db.Products, "ID_product", "Name", sales.ID_product);
            ViewBag.ID_staff = new SelectList(db.Staff, "ID_staff", "FIO", sales.ID_staff);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_sale,ID_product,ID_staff,Data_sale")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_product = new SelectList(db.Products, "ID_product", "Name", sales.ID_product);
            ViewBag.ID_staff = new SelectList(db.Staff, "ID_staff", "FIO", sales.ID_staff);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            db.Sales.Remove(sales);
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
