using System.ComponentModel.DataAnnotations;

namespace HospitalSystemBlazor.Entities.Models
{
    public class Especialidad:BaseModel
    {
        [Key]
        public int IdEspecialidad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public virtual ICollection<Doctor> Doctores { get; set; }
    }
}
