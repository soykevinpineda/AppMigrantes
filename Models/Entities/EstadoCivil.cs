using System.ComponentModel.DataAnnotations;

namespace Migrantes.Models.Entities
{
    public class EstadoCivil
    {
        [Key]
        public int id_estado_civil { get; set; }

        public string estado_civil { get; set; }

        public int activo { get; set; }
    }
}
