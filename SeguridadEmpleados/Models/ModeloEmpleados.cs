using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguridadEmpleados.Models
{
    public class ModeloEmpleados
    {
        ContextoEmpleados contexto;
        public ModeloEmpleados()
        {
            contexto = new ContextoEmpleados();
        }

        public EMP ExisteEmpleado(String apellido, int empno)
        {
            var consulta = from datos in contexto.EMP
                           where datos.APELLIDO == apellido
                           && datos.EMP_NO == empno
                           select datos;
            return consulta.FirstOrDefault();
        }

        public EMP BuscarEmpleado(int empno)
        {
            var consulta = from datos in contexto.EMP
                           where datos.EMP_NO == empno
                           select datos;
            return consulta.FirstOrDefault();
        }

        public List<EMP> GetEmpleadosSubordinados(int iddirector)
        {
            var consulta = from datos in contexto.EMP
                           where datos.DIR == iddirector
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return consulta.ToList();
            }
        }

        public List<EMP> GetEmpleados()
        {
            var consulta = from datos in contexto.EMP
                           select datos;
            return consulta.ToList();
        }

        public void ModificarEmpleado(int empno, String apellido
            , String oficio, int salario)
        {
            EMP empleado = this.BuscarEmpleado(empno);
            empleado.APELLIDO = apellido;
            empleado.OFICIO = oficio;
            empleado.SALARIO = salario;
            this.contexto.SaveChanges();
        }
    }
}