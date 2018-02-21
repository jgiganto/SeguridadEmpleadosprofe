using SeguridadEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SeguridadEmpleados
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public void Application_PostAuthenticateRequest
                (object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["cookieempleado"];
            if (cookie != null)
            {
                String datoscookie = cookie.Value;
                FormsAuthenticationTicket ticket =
                    FormsAuthentication.Decrypt(datoscookie);
                String idempleado = ticket.Name;
                String oficio = ticket.UserData;
                ModeloEmpleados modelo = new ModeloEmpleados();
                EMP emp = modelo.BuscarEmpleado(int.Parse(idempleado));
                //IDENTIDAD
                GenericIdentity identidad = new GenericIdentity(emp.APELLIDO);
                //ROLES U OFICIOS DEL EMPLEADO
                List<String> oficios = new List<string>() { oficio };
                EmpleadoPrincipal empleado = new EmpleadoPrincipal(identidad, oficios);
                empleado.Apellido = emp.APELLIDO;
                empleado.Oficio = emp.OFICIO;
                empleado.FechaAlta = emp.FECHA_ALT.GetValueOrDefault();
                empleado.NumeroEmpleado = emp.EMP_NO;
                //ALMACENAMOS EL USUARIO PRINCIPAL EN LA SESION
                HttpContext.Current.User = empleado;
            }
        }

        
    }
}
