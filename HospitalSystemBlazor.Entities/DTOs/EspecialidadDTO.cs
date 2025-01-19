namespace HospitalSystemBlazor.Entities.DTOs
{
    public class EspecialidadDTO
    {
        public int IdEspecialidad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
    }
}
