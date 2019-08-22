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
    public class SupplierController : Controller
    {
        private DotNetAssignmentsEntities db = new DotNetAssignmentsEntities();

        // GET: Supplier
        public ActionResult Index()
        {
            return View(db.SupplierInfoes.ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          

            SupplierInfo supplierInfo = db.SupplierInfoes.Find(id);
            if (supplierInfo == null)
            {
                return HttpNotFound();
            }
            return View(supplierInfo);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierName,Address,City,ContactNo,Email")] SupplierInfo supplierInfo)
        {
            if (ModelState.IsValid)
            {
                db.SupplierInfoes.Add(supplierInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplierInfo);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierInfo supplierInfo = db.SupplierInfoes.Find(id);
            if (supplierInfo == null)
            {
                return HttpNotFound();
            }
            return View(supplierInfo);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierName,Address,City,ContactNo,Email")] SupplierInfo supplierInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplierInfo);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierInfo supplierInfo = db.SupplierInfoes.Find(id);
            if (supplierInfo == null)
            {
                return HttpNotFound();
            }
            return View(supplierInfo);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierInfo supplierInfo = db.SupplierInfoes.Find(id);
            db.SupplierInfoes.Remove(supplierInfo);
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
