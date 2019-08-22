using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServiceMVCClient.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
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

        public ActionResult UsingHTTP(string opt, string name)//(FormCollection Form)//
        {
            using (HelloService.HelloServiceClient serviceClient = new HelloService.HelloServiceClient("usingBasicHttp"))
            {
                if (opt == "SayHello") // sayHello
                {
                    
                    ViewBag.ServiceResponse = "Using HTTP: " + serviceClient.SayHello(name);
                }
                else
                {
                    ViewBag.ServiceResponse = "Using HTTP: " + serviceClient.TodayProgram(name);
                }


            }
            return View("Index");
        }
        public ActionResult UsingTCP(string opt,string name)
        {
            using (HelloService.HelloServiceClient serviceClient = new HelloService.HelloServiceClient("usingNetTcp"))
            {
                if (opt== "SayHello") // sayHello
                {
                     ViewBag.ServiceResponse = "Using TCP: "+ serviceClient.SayHello(name);
                }
                else
                {
                     ViewBag.ServiceResponse = "Using TCP: " + serviceClient.TodayProgram(name);
                }
            }
            return View("Index");
        }

        public ActionResult JobList(string role)
        {
            JobOpenings.JobOpeningsClient client = new JobOpenings.JobOpeningsClient();
            List<JobOpenings.Jobs> jobList = null;
            if (string.IsNullOrEmpty(role))
            {
                  jobList = client.GetAvailbleJobList();
            }
            else
            {
                jobList = client.GetJobsByRole(Server.HtmlEncode(role));
            }
           
            return View(jobList);
        }

        public ActionResult Calculator(string calcOperator,int first=0, int second=0)
        {
            using (CalcService.CalculatorClient client=new CalcService.CalculatorClient())
            {
                decimal result = 0;
                switch (calcOperator)
                {
                    case "+":
                        result = client.Addition(first, second);
                        break;
                    case "-":
                        result = client.Substraction(first, second);
                        break;
                    case "*":
                        result = client.Multiplication(first, second);
                        break;
                    case "/":
                        result = client.Division(first, second);
                        break;
                    default:
                        break;
                }
                @ViewBag.CalcResult = string.IsNullOrEmpty(calcOperator)? string.Empty: $"{first} {calcOperator} {second} = {result}";

            }
            return View();
        }
    }
}