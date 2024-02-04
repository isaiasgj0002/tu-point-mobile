using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TuPoint.Models
{
    public class User
    {
        [PrimaryKey]
        public int cod_visitante { get; set; }
        [MaxLength(50)]
        public string nombre_apellido { get; set; }
        [MaxLength(50)]
        public string correo { get; set; }
        [MaxLength(50)]
        public string telefono { get; set; }
        [MaxLength(50)]
        public string nacimiento { get; set; }
        [MaxLength(50)]
        public string dni { get; set; }
        [MaxLength(50)]
        public string clave { get; set; }
        [MaxLength(50)]
        public string imagen { get; set; }
        [MaxLength(50)]
        public string incripcion { get; set; }
        [MaxLength(50)]
        public string latitud_visitante { get; set; }
        [MaxLength(50)]
        public string longitud_visitante { get; set; }
        [MaxLength(50)]
        public int estado_visitante { get; set; }
        [MaxLength(50)]
        public string hora_coneccion { get; set; }
        [MaxLength(50)]
        public string monedas_tupoint { get; set; }
        [MaxLength(50)]
        public int genero { get; set; }
        [MaxLength(50)]
        public string id_referido { get; set; }
        [MaxLength(50)]
        public string direccion { get; set; }
        [MaxLength(50)]
        public int nivel { get; set; }
        [MaxLength(50)]
        public string cod_area { get; set; }
        [MaxLength(50)]
        public int cod_referido { get; set; }
        [MaxLength(50)]
        public int verificado { get; set; }
        [MaxLength(50)]
        public int tipo { get; set; }
    }
}
