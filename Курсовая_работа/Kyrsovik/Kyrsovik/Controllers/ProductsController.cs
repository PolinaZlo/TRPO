using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kyrsovik.Models;
using Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;
using Table = Microsoft.Office.Interop.Word.Table;
using Word = Microsoft.Office.Interop.Word;

namespace Kyrsovik.Controllers
{
    public class ProductsController : Controller
    {
        private Polina_ZlobinaEntities db = new Polina_ZlobinaEntities();
        string templateFile = @"D:\Kyrsovik_TRPO\Kyrsovik\example.docx";
        string templateFilere = @"D:\Kyrsovik_TRPO\Kyrsovik\rep.docx";

        // GET: Products
        public ActionResult Index(string sortOrder, string searchString, 
            string non, string b, int? Catygories)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "name_desc";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            var products = from s in db.Products select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                case "Status":
                    products = products.OrderBy(s => s.ID_status);
                    break;
                case "status_desc":
                    products = products.OrderByDescending(s => s.ID_status);
                    break;
                default:
                    products = products.OrderBy(s => s.ID_product);
                    break;
            }

            ViewBag.Catygories = db.Catygories.ToList();

            if (b != null)
            {
                products = products.Where(s => s.ID_category == Catygories);
            }



         /*   var name_report = "Отчет по товарам";
            DateTime thisDay = DateTime.Today;
            var date = thisDay.ToString("d");
            var wordApp = new Word.Application();
            wordApp.Visible = false;
           try
            {
                var wordDoc = wordApp.Documents.Open(templateFile);
                ReplaceWord("{name}", name_report, wordDoc);
                ReplaceWord("{date}", date, wordDoc);



                //  CreateTable(ToDataTable(products.ToList), wordDoc);
              //  String [] fuck = new String;
               // products.ToArray();
                //products.ToList();


                wordDoc.SaveAs2(@"D:\Kyrsovik_TRPO\Kyrsovik\report.docx");
              //  wordApp.Visible = true;
            }
            catch (Exception error)
            {  }*/

            return View(products.ToList());
        }

      
      
        private void ReplaceWord(string toReplace, string text, Word.Document wordDoc)
        {
            var range = wordDoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: toReplace, ReplaceWith: text);
        }







     /*public void Receipt()
        {
            Products products = db.Products.Find(id);
            var name_report = "Чек";
            DateTime thisDay = DateTime.Today;
            var date = thisDay.ToString("d");
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                var wordDoc = wordApp.Documents.Open(templateFilere);
                ReplaceWord("{name}", name_report, wordDoc);
                ReplaceWord("{date}", date, wordDoc);
                ReplaceWord("{tovar}", products.Name, wordDoc);
                ReplaceWord("{price}", products.Price.ToString(), wordDoc);
                ReplaceWord("{caty}", products.Catygories.Name_category, wordDoc);
                wordDoc.SaveAs2(@"D:\Kyrsovik_TRPO\Kyrsovik\receipt.docx");
                wordApp.Visible = true;
            }
            catch (Exception error)
            { }
            
        }*/








        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ID_category = new SelectList(db.Catygories, "ID_category", "Name_category");
            ViewBag.ID_owner = new SelectList(db.Owners, "ID_owner", "FIO");
            ViewBag.ID_status = new SelectList(db.Statuses, "ID_status", "Name_status");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_product,ID_owner,ID_status,ID_category,Name,Price,Photo,Num_off,Data_start,Data_end,Commentary")] Products products, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        products.Photo = reader.ReadBytes(upload.ContentLength);
                    }
                }
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_category = new SelectList(db.Catygories, "ID_category", "Name_category", products.ID_category);
            ViewBag.ID_owner = new SelectList(db.Owners, "ID_owner", "FIO", products.ID_owner);
            ViewBag.ID_status = new SelectList(db.Statuses, "ID_status", "Name_status", products.ID_status);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_category = new SelectList(db.Catygories, "ID_category", "Name_category", products.ID_category);
            ViewBag.ID_owner = new SelectList(db.Owners, "ID_owner", "FIO", products.ID_owner);
            ViewBag.ID_status = new SelectList(db.Statuses, "ID_status", "Name_status", products.ID_status);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_product,ID_owner,ID_status,ID_category,Name,Price,Photo,Num_off,Data_start,Data_end,Commentary")] Products products, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(products).State = EntityState.Modified;
                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            products.Photo = reader.ReadBytes(upload.ContentLength);
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(products).Property(m => m.Photo).IsModified = false;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                ViewBag.ID_category = new SelectList(db.Catygories, "ID_category", "Name_category", products.ID_category);
                ViewBag.ID_owner = new SelectList(db.Owners, "ID_owner", "FIO", products.ID_owner);
                ViewBag.ID_status = new SelectList(db.Statuses, "ID_status", "Name_status", products.ID_status);
                return View(products);
            }
            catch (Exception e) { return View(products); }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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
