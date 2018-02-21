using SeguridadEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SeguridadEmpleados.Controllers
{
    public class ValidacionController : Controller
    {
        // GET: ErrorAcceso
        public ActionResult ErrorAcceso()
        {
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        public ActionResult Login(String usuario, int password)
        {
            ModeloEmpleados modelo = new ModeloEmpleados();
            EMP empleado = modelo.ExisteEmpleado(usuario, password);
            if (empleado == null)
            {
                ViewBag.Mensaje = "Usuario/Password incorrectos";
                return View();
            }
            else
            {
                FormsAuthenticationTicket ticket = 
                    new FormsAuthenticationTicket(1, empleado.EMP_NO.ToString()
                    , DateTime.Now, DateTime.Now.AddHours(1)
                    , true, empleado.OFICIO, FormsAuthentication.FormsCookiePath);
                String datoscifrados = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie("cookieempleado", datoscifrados);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Empleados");
            }
        }

        


    }
}