using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migrantes.Models.Entities
{
    public class DatosFamiliares
    {
        [Key]
        public int id_datos_familiares { get; set; }

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }


        public string nombres_madre { get; set; }
        public string primer_apellido_madre { get; set; }
        public string segundo_apellido_madre { get; set; }
        public int edad_madre { get; set; }
        public string profesion_madre { get; set; }


        public string nombres_padre { get; set; }
        public string primer_apellido_padre { get; set; }
        public string segundo_apellido_padre { get; set; }
        public int edad_padre { get; set; }
        public string profesion_padre { get; set; }


        public int estado_datosfamiliares { get; set; }
        public virtual Persona PersonaLink { get; set; }
    }
}
