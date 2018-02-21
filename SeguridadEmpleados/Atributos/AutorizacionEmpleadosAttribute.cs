using SeguridadEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SeguridadEmpleados.Atributos
{
    public class AutorizacionEmpleadosAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization
            (AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                EmpleadoPrincipal empleado =
                    HttpContext.Current.User as EmpleadoPrincipal;
                if (empleado.IsInRole("PRESIDENTE") == false
                    && empleado.IsInRole("DIRECTOR") == false
                    && empleado.IsInRole("ANALISTA") == false)
                {
                    //SI EL USUARIO NO ES ADMINISTRADOR
                    //LE ENVIAMOS A OTRA RUTA (ErrorAcceso)
                    RouteValueDictionary rutaacceso =
                        new RouteValueDictionary(new
                        {
                            controller = "Validacion"
                            ,
                            action = "ErrorAcceso"
                        });
                    RedirectToRouteResult direccionacceso =
                        new RedirectToRouteResult(rutaacceso);
                    filterContext.Result = direccionacceso;
                }
            }
            else
            {
                //EL USUARIO NO SE HA VALIDADO TODAVIA
                //Y HACEMOS UN ROUTING HACIA EL LOGIN
                //PARA HACER UN ROUTING (INTERCEPTAR LA PETICION)
                //NECESITAMOS CREAR LA CLASE 
                //RouteValueDictionary CON CONTROLLER Y ACTION
                //DONDE DESEAMOS ENVIAR LA NUEVA PETICION
                RouteValueDictionary ruta =
                    new RouteValueDictionary(new
                    {
                        controller = "Validacion"
                        ,
                        action = "Login"
                    });
                //MEDIANTE LA CLASE RedirectToRouteResult
                //INDICAMOS LA PROPIA REDIRECCION CON LA RUTA
                RedirectToRouteResult direccionlogin =
                    new RedirectToRouteResult(ruta);
                //MEDIANTE filtercontext, TENEMOS UNA
                //PROPIEDAD RESULT QUE NOS PERMITE REDIRIGIR
                //A OTRAS RUTAS
                filterContext.Result = direccionlogin;
            }
        }
    }
}
