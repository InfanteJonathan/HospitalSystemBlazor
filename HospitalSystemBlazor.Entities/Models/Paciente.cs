using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemBlazor.Entities.Models
{
    public class Paciente:BaseModel
    {
        [Key]
        public int IdPaciente { get; set; }
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }

        [Range(1,9)]
        public int?  NumContacto { get; set; }
        public string? Direccion { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
    }
}
