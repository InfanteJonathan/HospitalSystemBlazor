using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemBlazor.Entities.Models
{
    public class Usuario:BaseModel
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public virtual Roles Roles { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
