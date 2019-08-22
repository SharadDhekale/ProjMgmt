using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ADOAssignment.Models;

namespace ADOAssignment.Controllers
{
    public class CategoriesController : Controller
    {
        private DotNetAssignmentsEntities db = new DotNetAssignmentsEntities();

        // GET: Categories
        public ActionResult Index(string SearchBy, string txtSearchCriteria)
        {
            if (string.IsNullOrEmpty(SearchBy) && string.IsNullOrEmpty(txtSearchCriteria))
            {
                var categories = db.Categories.Include(c => c.SupplierInfo);
                return View(categories.ToList());
            }
            else
            {
                var categories = db.Categories.Include(c => c.SupplierInfo);
                IEnumerable<Category> _filteredList = null;
                switch (SearchBy)
                {
                    case "Division":
                        _filteredList = categories.Where(x => x.Division.StartsWith(txtSearchCriteria));
                        return View(_filteredList.ToList());
                    case "Supplier_id":
                        _filteredList = categories.Where(x => x.supplier_id== int.Parse(txtSearchCriteria));
                        return View(_filteredList.ToList());
                    case "Supplier_Name":
                        _filteredList = categories.Where(x => x.SupplierInfo.SupplierName.StartsWith(txtSearchCriteria));
                        return View(_filteredList.ToList());
                    case "category_code":
                        _filteredList = categories.Where(x => x.Category_code.StartsWith(txtSearchCriteria));
                        return View(_filteredList.ToList());
                    default:
                        return View(_filteredList.ToList());
                }
               
               
            }
         
        }

        // GET: Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.supplier_id = new SelectList(db.SupplierInfoes, "Id", "SupplierName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category_code,Category_name,Division,Region,supplier_id")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supplier_id = new SelectList(db.SupplierInfoes, "Id", "SupplierName", category.supplier_id);
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.supplier_id = new SelectList(db.SupplierInfoes, "Id", "SupplierName", category.supplier_id);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_code,Category_name,Division,Region,supplier_id")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supplier_id = new SelectList(db.SupplierInfoes, "Id", "SupplierName", category.supplier_id);
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
