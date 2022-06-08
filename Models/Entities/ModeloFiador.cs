using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migrantes.Models.Entities
{
    public class ModeloFiador
    {

        [Key]
        public int IdFiador { get; set; }

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }


        [Display(Name = ("Primer nombre"))]
        public string PrimerNombreFiador { get; set; }

        [Display(Name = ("Segundo nombre"))]
        public string SegundoNombreFiador { get; set; }

        [Display(Name = ("Primer apellido"))]
        public string PrimerApellidoFiador { get; set; }


        [Display(Name = ("Segundo apellido"))]
        public string SegundoApellidoFiador { get; set; }

        [Display(Name = ("País de nacimiento"))]
        public string PaisNacimientoFiador { get; set; }

        [Display(Name = ("Edad"))]
        public int EdadFiador { get; set; }

        [Display(Name = ("Sexo"))]
        public int SexoFiador { get; set; }
  
        [Display(Name = ("Email"))]
        public string EmailFiador { get; set; }

        [Display(Name = ("Teléfono movil"))]
        public int TelefonoFiador { get; set; }

        [Display(Name = ("Teléfono movil alternativo"))]
        public int TelefonoAlternoFiador { get; set; }


        [Display(Name = ("Nro. Cartas de recomendación personal"))]
        public int NumCartasPersonales { get; set; }


        [Display(Name = ("Nro. Cartas de recomendación familiar"))]
        public int NumCartasFamiliares { get; set; }

        [Display(Name = ("Entrego recibo de agua o luz?"))]
        public bool EntregoRecibo_Agua_o_Luz { get; set; }


        [Display(Name = ("Fecha de grabación"))]
        [DataType(DataType.DateTime)]
        public DateTime FechaGrabacionFiador { get; set; } = DateTime.Now;

        public virtual Persona Persona { get; set; }

    }
}
