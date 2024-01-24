using System;
using System.Collections.Generic;
using System.Text;

namespace TuPoint.Models
{
    public class Company
    {
        public string cod_emp { get; set; }
        public string nombre_emp { get; set; }
        public string descripcion { get; set; }
        public int miles { get; set; }
        public string foto { get; set; }
        public string direccion { get; set; }
        public double latitud_emp { get; set; }
        public double longitud_emp { get; set; }
        public string telefono { get; set; }
    }
}
