namespace HospitalSystemBlazor.Entities.Models
{
    public class BaseModel
    {
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool Activo { get; set; }
    }
}
