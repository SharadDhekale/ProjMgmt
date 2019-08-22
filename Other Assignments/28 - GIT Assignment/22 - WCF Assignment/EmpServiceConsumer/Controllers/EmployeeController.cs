using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmpServiceConsumer.EmpService;
using System.Net;

namespace EmpServiceConsumer.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeServiceClient _empClient = new EmployeeServiceClient();
        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> EmpList = _empClient.RetreiveEmployees();
            return View(EmpList);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var emp = _empClient.RetreiveEmployeeByID(id);
            return View(emp);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "EmpNumber,FirstName,LastName,Dept")] Employee emp)
        {
            try
            {
                // TODO: Add insert logic here
                var result=_empClient.AddEmployee(emp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var emp = _empClient.RetreiveEmployeeByID(id.Value);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpNumber,FirstName,LastName,Dept")] Employee emp)
        {
            if (ModelState.IsValid)
            {
                _empClient.UpdateEmployee(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var emp = _empClient.RetreiveEmployeeByID(id.Value);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var reult = _empClient.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

      
    }
}
