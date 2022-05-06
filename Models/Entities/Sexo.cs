using System.ComponentModel.DataAnnotations;

namespace Migrantes.Models.Entities
{
    public class Sexo
    {
        [Key]
        public int id_sexo { get; set; }

        [MaxLength(50)]
        public string nombre_sexo { get; set; }

        [MaxLength(1)]
        public string nomenclatura_sexo { get; set; }

        public int activo { get; set; }
    }
}
