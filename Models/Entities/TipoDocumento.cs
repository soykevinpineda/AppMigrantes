using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Migrantes.Models.Entities
{
    public class TipoDocumento
    {
        [Key]
        public int tid_id_documento { get; set; }

        [MaxLength(100)]
        public string tid_descripcion { get; set; }

        public int tid_activo { get; set; }

        public virtual ICollection<IdentidadPersona> IdentidadPersonaLink { get; set; }




        public static implicit operator TipoDocumento(string v)
        {
            throw new NotImplementedException();
        }
    }
}
