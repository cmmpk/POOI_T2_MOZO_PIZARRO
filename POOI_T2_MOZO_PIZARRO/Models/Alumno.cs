using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POOI_T2_MOZO_PIZARRO.Models
{
    public class Alumno
    {
        public string dni { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string carrera { get; set; }

        public string ciclo { get; set; }

        public Alumno() { }

        public Alumno(string dni, string nombres, string apellidos, string carrera, string ciclo) { }
    }
}