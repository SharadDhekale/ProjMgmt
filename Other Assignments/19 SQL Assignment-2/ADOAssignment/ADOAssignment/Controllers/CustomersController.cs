using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ADOAssignment.Models;
using System.IO;

namespace ADOAssignment.Controllers
{
    public class CustomersController : Controller
    {
         DBCommunicator dbconnect = new DBCommunicator();

        // GET: Customers
        //public ActionResult Index()
        public ActionResult Index(string txtSearchCriteria)
        {
            List<Customer> custlist = null;
            if (!string.IsNullOrEmpty(txtSearchCriteria))
            {
                DateTime _date = Convert.ToDateTime(txtSearchCriteria);
                custlist = dbconnect.CustomerListByDate(_date);
            }
            else
            {
                custlist = dbconnect.CustomerList();
            }
            return View(custlist);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer =  dbconnect.CustomerDetails(id.Value);
            string xml= XmlHelper.GetXMLFromObject(customer);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string xmlstring = new StreamReader(file.InputStream).ReadToEnd();
            Customer custObj= (Customer) XmlHelper.ObjectToXML(xmlstring, typeof(Customer));
            dbconnect.AddCustomer(custObj);
            return RedirectToAction("Index");
        }
        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Custid,Custname,CustAddress,DOB,Salary")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                dbconnect.AddCustomer(customer);
               
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = dbconnect.CustomerDetails(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Custid,Custname,CustAddress,DOB,Salary")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                dbconnect.UpdateCustomer(customer);
                 return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer =dbconnect.CustomerDetails(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             dbconnect.DeleteCustomer(id);
             return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }
    }
}
