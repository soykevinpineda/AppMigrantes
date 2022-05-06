using System.ComponentModel.DataAnnotations;

namespace Migrantes.Models.Entities
{
    public class TipoEstado
    {
        [Key]
        public int tep_tipo_estado { get; set; }

        [MaxLength(100)]
        public string tep_descripcion { get; set; }

        public int tep_activo { get; set; }
    }
}
