using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemBlazor.Entities.Models
{
    public class DiaTrabajo
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdDia { get; set; }
        public int IdHorario { get; set; }
        public DayOfWeek Dia { get; set; }

        [ForeignKey("IdHorario")]
        public virtual Horario Horario { get; set; }
    }
}
