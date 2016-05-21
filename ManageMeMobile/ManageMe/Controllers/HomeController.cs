using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ManageMeDomainEntity; 

namespace ManageMe.Controllers
{
    public class HomeController : Controller
    {
        private ManageMeModel db = new ManageMeModel();
       
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
    }
}