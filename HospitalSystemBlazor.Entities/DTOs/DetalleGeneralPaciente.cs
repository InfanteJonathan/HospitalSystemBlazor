using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystemBlazor.Entities.DTOs
{
    public class DetalleGeneralPaciente
    {
        public int IdPaciente { get; set; }
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }

        [Range(1, 9)]
        public int? NumContacto { get; set; }
        public string? Direccion { get; set; }
    }
}
