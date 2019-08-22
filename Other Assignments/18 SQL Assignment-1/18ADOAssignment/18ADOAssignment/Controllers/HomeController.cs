using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _18ADOAssignment.Models;
namespace _18ADOAssignment.Controllers
{
    public class HomeController : Controller
    {
        DBCommunicator dbCon = new DBCommunicator();

        public ActionResult Index()
        {
          
            var supplierList= dbCon.GetSupplierList();
            return View(supplierList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductDetails()
        {
            var productList = dbCon.GetProductsList();
            return View(productList);
        }

        public ActionResult Suppliers()
        {
            var productList = dbCon.GetProductsList();
            return View();
        }
    }
}