using SeguridadEmpleados.Atributos;
using SeguridadEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SeguridadEmpleados.Controllers
{
    [AutorizacionEmpleados]

    public class EmpleadosController : Controller
    {
        ModeloEmpleados modelo;

        public EmpleadosController()
        {
            this.modelo = new ModeloEmpleados();
        }

        // GET: Empleados
        public ActionResult Index()
        {
            //RECUPERAMOS AL EMPLEADOS DE LA SESION
            EmpleadoPrincipal empleado =
                HttpContext.User as EmpleadoPrincipal;
            List<EMP> lista;
            if (empleado.IsInRole("PRESIDENTE") == true)
            {
                lista = modelo.GetEmpleados();
            }
            else
            {
                lista = modelo.GetEmpleadosSubordinados(empleado.NumeroEmpleado);
            }
            return View(lista);
        }

        public ActionResult Editar(int empno)
        {
            EMP empleado = modelo.BuscarEmpleado(empno);
            return View(empleado);
        }

        [HttpPost]
        public ActionResult Editar(int empno, String apellido
            , String oficio, int salario)
        {
            modelo.ModificarEmpleado(empno, apellido, oficio, salario);
            return RedirectToAction("Index");
        }

        public ActionResult CerrarSesion()
        {
            HttpContext user = null;
            FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies["cookieempleado"];
            cookie.Expires = DateTime.Now.AddDays(-3);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }
    }
}