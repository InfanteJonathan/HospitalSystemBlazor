using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemBlazor.Entities.Models
{
    public class Horario:BaseModel
    {
        [Key]
        public int IdHorario { get; set; }
        public int IdDoctor { get; set; }
        public TimeOnly  HoraInicio { get; set; }
        public TimeOnly HoraFinal { get; set; }

        [ForeignKey("IdDoctor")]
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<DiaTrabajo> DiaTrabajos { get; set; }

    }
}
