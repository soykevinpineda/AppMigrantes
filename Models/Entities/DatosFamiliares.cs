using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migrantes.Models.Entities
{
    public class DatosFamiliares
    {
        [Key]
        public int Id_DatosFamiliares { get; set; }

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }

        public string PrimerNombreDeLaMadre { get; set; }

        public string SegundoNombreDeLaMadre { get; set; }

        public string ApellidosDeLaMadre { get; set; }


        [Display(Name = ("Fecha de nacimiento"))]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoDeLaMadre { get; set; }

        [Display(Name = ("Edad"))]
        public int EdadDeLaMadre { get; set; }


        [Display(Name = ("País de nacimiento"))]
        [MaxLength(50)]
        public string PaisNacimientoDeLaMadre { get; set; }

        public string ProfesionDeLaMadre { get; set; }






        public string PrimerNombreDelPadre { get; set; }

        public string SegundoNombreDelPadre { get; set; }

        public string ApellidosDelPadre { get; set; }


        [Display(Name = ("Fecha de nacimiento"))]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoDelPadre { get; set; }


        [Display(Name = ("Edad"))]
        public int EdadDelPadre { get; set; }


        [Display(Name = ("País de nacimiento"))]
        [MaxLength(50)]
        public string PaisNacimientoDelPadre { get; set; }


        public string ProfesionDelPadre { get; set; }


        [Display(Name = ("Fecha de grabación"))]
        [DataType(DataType.DateTime)]
        public DateTime FechaGrabacionDeFamiliares { get; set; } = DateTime.Now;

        public int EstadoDeDatosFamiliares { get; set; }

        public virtual Persona PersonaLink { get; set; }
    }
}
