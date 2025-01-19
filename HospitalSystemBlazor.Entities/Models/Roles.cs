using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemBlazor.Entities.Models
{
    public class Roles
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
