using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Migrantes.Models.Entities
{
    public class Parientes
    {
        [Key]
        public int ParienteID { get; set; }

        [Required(ErrorMessage = "Seleccione un pariente")]
        [MaxLength(100)]
        public string DescripcionPariente { get; set; }

        public int Estado { get; set; }

        public virtual ICollection<DatosFamiliares> DatosFamiliaresLink { get; set; }
    }
}
