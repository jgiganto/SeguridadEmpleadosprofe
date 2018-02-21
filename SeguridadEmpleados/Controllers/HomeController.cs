using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace SeguridadEmpleados.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            String cadena =
                ConfigurationManager.ConnectionStrings["Tajamar"].ConnectionString;
            ViewBag.Cadena = cadena;
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