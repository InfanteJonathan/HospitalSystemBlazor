using System.ComponentModel.DataAnnotations;

namespace HospitalSystemBlazor.Entities.DTOs
{
    public class EspecialidadDTO
    {
        public int IdEspecialidad { get; set; }

        [Required(ErrorMessage ="El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Descripcion es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio")]
        [Range(1,500,ErrorMessage ="El Precio debe tener un valor entre 1  y 500")]
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
    }
}
