using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemBlazor.Entities.Models
{
    public class Doctor:BaseModel
    {
        [Key]
        public int IdDoctor { get; set; }
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public int? IdEspecialidad { get; set; }

        [Range(1,9,ErrorMessage ="Error al ingresar el telefono")]
        public int? Telefono { get; set; }

        [EmailAddress(ErrorMessage ="Ingrese un email correcto")]
        public string? Correo { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("IdEspecialidad")]
        public virtual Especialidad Especialidad { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }

    }
}
