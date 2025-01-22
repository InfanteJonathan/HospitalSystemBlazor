using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystemBlazor.Entities.DTOs
{
    public class DetalleGeneralDoctor
    {
        public int IdDoctor { get; set; }
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public int? IdEspecialidad { get; set; }

        [Range(1, 9, ErrorMessage = "Error al ingresar el telefono")]
        public int? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un email correcto")]
        public string? Correo { get; set; }
    }
}
