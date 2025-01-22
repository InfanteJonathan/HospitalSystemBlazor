using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystemBlazor.Entities.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string IdRol { get; set; }
        public bool Activo { get; set; }
    }
}
