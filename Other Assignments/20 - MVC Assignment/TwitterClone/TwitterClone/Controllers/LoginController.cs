using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //UserRepository db = new UserRepository();
        TwitterCloneEntities db = new TwitterCloneEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string uname, string pwd)
        {
            Person p = db.People.Find(uname);
            
            if (p != null && p.password.Equals(pwd))
            {
                if(p.user_id.Equals("admin") && p.password.Equals("admin"))
                {
                    return RedirectToAction("Index", "Person", new { p.user_id });
                }
                else
                {
                    TempData["FullName"] = p.fullName;
                    Session["FullName"] = p.fullName;
                    return RedirectToAction("Index", "Tweet", new { p.user_id });
                    //ViewData["err"] = "Success !!";
                    //return View();
                }
            }
            else
            {
                ViewData["err"] = "Invalid Login Credentials";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                //db.Add(user);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
            
        }

    }
}