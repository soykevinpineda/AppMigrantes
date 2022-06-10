using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migrantes.Models.Entities
{
    public class ModeloFiador
    {
        [ValidateNever]
        [Key]
        public int IdFiador { get; set; }

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese nombre del fiador.")]
        [Display(Name = ("Primer nombre"))]
        public string PrimerNombreFiador { get; set; }

        [Required(ErrorMessage = "Por favor ingrese segundo nombre del fiador.")]
        [Display(Name = ("Segundo nombre"))]
        public string SegundoNombreFiador { get; set; }

        [Required(ErrorMessage = "Por favor ingrese primer apellido del fiador.")]
        [Display(Name = ("Primer apellido"))]
        public string PrimerApellidoFiador { get; set; }

        [Required(ErrorMessage = "Por favor ingrese segundo apellido del fiador.")]
        [Display(Name = ("Segundo apellido"))]
        public string SegundoApellidoFiador { get; set; }


        [Display(Name = ("País de nacimiento"))]
        public string PaisNacimientoFiador { get; set; }

        [Display(Name = ("Sexo"))]
        public int SexoFiador { get; set; }

        [Display(Name = ("Edad"))]
        public int EdadFiador { get; set; }

        [EmailAddress]
        [Display(Name = ("Email"))]
        public string EmailFiador { get; set; }



        [Phone]
        [Display(Name = ("Teléfono movil"))]
        public int TelefonoFiador { get; set; }

        [Phone]
        [Display(Name = ("Teléfono movil alternativo"))]
        public int TelefonoAlternoFiador { get; set; }

        [Required]
        [Range(0, 3)]
        [Display(Name = ("Nro. Cartas de recomendación personal"))]
        public int NumCartasPersonales { get; set; }

        [Required]
        [Range(0, 3)]
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
