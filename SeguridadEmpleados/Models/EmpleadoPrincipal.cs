using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace SeguridadEmpleados.Models
{
    public class EmpleadoPrincipal : IPrincipal
    {
        public String Apellido { get; set; }
        public int NumeroEmpleado { get; set; }
        public String Oficio { get; set; }
        public DateTime FechaAlta { get; set; }

        List<String> Roles;

        public EmpleadoPrincipal(IIdentity identidad, List<String> roles)
        {
            this.Roles = roles;
            this.Identity = identidad;
        }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            bool existe = this.Roles.Contains(role);
            return existe;
        }
    }
}