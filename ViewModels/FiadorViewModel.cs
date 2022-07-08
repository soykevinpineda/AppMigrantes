using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migrantes.ViewModels
{
    public class FiadorViewModel
    {
        [ValidateNever]
        [Key]
        public int IdFiador { get; set; }

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }

        [Required(ErrorMessage = "Introduce el primer nombre")]
        [Display(Name = ("Primer nombre"))]
        public string PrimerNombreDelFiador { get; set; }

        [Required(ErrorMessage = "Introduce el segundo nombre")]
        [Display(Name = ("Segundo nombre"))]
        public string SegundoNombreDelFiador { get; set; }

        [Required(ErrorMessage = "Introduce los apellidos")]
        [Display(Name = ("Apellidos"))]
        public string ApellidosDelFiador { get; set; }

        [Required(ErrorMessage = "Elige una fecha de nacimiento")]
        [Display(Name = ("Fecha de nacimiento"))]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoDelFiador { get; set; }

        [Display(Name = ("Edad"))]
        public int EdadDelFiador { get; set; }

        [Required(ErrorMessage = "Elige un género")]
        [Display(Name = ("Género"))]
        public int SexoDelFiador { get; set; }

        [Display(Name = ("País de nacimiento"))]
        public string PaisNacimientoDelFiador { get; set; }

        [EmailAddress]
        [Display(Name = ("E-mail"))]
        [DataType(DataType.EmailAddress)]
        public string EmailFiador { get; set; }

        [Required(ErrorMessage = "Introduce un número teléfonico")]
        [Display(Name = ("Teléfono movil"))]
        public int TelefonoFiador { get; set; }


        [Display(Name = ("Teléfono movil alternativo"))]
        public int TelefonoAlternoFiador { get; set; }

        [Required(ErrorMessage = "Introduce la cantidad de cartas personales")]
        [Range(0, 3,ErrorMessage = "Debe ser un número entre 0 y 3")]
        [Display(Name = ("Nro. Cartas de recomendación personal"))]
        public int NumCartasPersonales { get; set; }


        [Required(ErrorMessage = "Introduce la cantidad de cartas familiares")]
        [Range(0, 3)]
        [Display(Name = ("Nro. Cartas de recomendación familiar"))]
        public int NumCartasFamiliares { get; set; }


        [Display(Name = ("Entrego recibo de agua o luz?"))]
        public bool EntregoRecibo_Agua_o_Luz { get; set; }


        [Display(Name = ("Fecha de grabación"))]
        [DataType(DataType.DateTime)]
        public DateTime FechaGrabacionDelFiador { get; set; } = DateTime.Now;

     
    }
}
