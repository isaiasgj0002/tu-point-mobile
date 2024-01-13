using System;
using System.Collections.Generic;
using System.Text;

namespace TuPoint.Models
{
    public class User
    {
        public int cod_visitante { get; set; }
        public string nombre_apellido { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string nacimiento { get; set; }
        public string dni { get; set; }
        public string clave { get; set; }
        public string imagen { get; set; }
        public string incripcion { get; set; }
        public string latitud_visitante { get; set; }
        public string longitud_visitante { get; set; }
        public int estado_visitante { get; set; }
        public string hora_coneccion { get; set; }
        public string monedas_tupoint { get; set; }
        public int genero { get; set; }
        public string id_referido { get; set; }
        public string direccion { get; set; }
        public int nivel { get; set; }
        public string cod_area { get; set; }
        public int cod_referido { get; set; }
        public int verificado { get; set; }
        public int tipo { get; set; }
    }
}
