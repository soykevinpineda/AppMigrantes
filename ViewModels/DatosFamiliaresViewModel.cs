
using Microsoft.AspNetCore.Http;
using Migrantes.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace Migrantes.ViewModels
{
    public class DatosFamiliaresViewModel
    {
        [Key]
        public int Id_DatosFamiliares { get; set; }

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }

        [Display(Name = ("Primer nombre"))]
        public string PrimerNombreDeLaMadre { get; set; }

        [Display(Name = ("Segundo nombre"))]
        public string SegundoNombreDeLaMadre { get; set; }

        [Display(Name = ("Apellidos"))]
        public string ApellidosDeLaMadre { get; set; }


        [Display(Name = ("Fecha de nacimiento"))]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoDeLaMadre { get; set; }

        [Display(Name = ("Edad"))]
        public int EdadDeLaMadre { get; set; }


        [Display(Name = ("País de nacimiento"))]
        [MaxLength(50)]
        public string PaisNacimientoDeLaMadre { get; set; }

        [Display(Name = ("Profesión"))]
        public string ProfesionDeLaMadre { get; set; }








        [Display(Name = ("Primer nombre"))]
        public string PrimerNombreDelPadre { get; set; }

        [Display(Name = ("Segundo nombre"))]
        public string SegundoNombreDelPadre { get; set; }

        [Display(Name = ("Apellidos"))]
        public string ApellidosDelPadre { get; set; }

        [Display(Name = ("Fecha de nacimiento"))]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoDelPadre { get; set; }


        [Display(Name = ("Edad"))]
        public int EdadDelPadre { get; set; }


        [Display(Name = ("País de nacimiento"))]
        [MaxLength(50)]
        public string PaisNacimientoDelPadre { get; set; }

        [Display(Name = ("Profesión"))]
        public string ProfesionDelPadre { get; set; }


        [Display(Name = ("Fecha de grabación"))]
        [DataType(DataType.DateTime)]
        public DateTime FechaGrabacionDeFamiliares { get; set; } = DateTime.Now;

        public int EstadoDeDatosFamiliares { get; set; }


        #region Modelo Personas

        [Display(Name = ("Primer nombre"))]
        [MaxLength(50)]
        public string per_primer_nom { get; set; }

        [Display(Name = ("Segundo nombre"))]
        [MaxLength(50)]
        public string per_segundo_nom { get; set; }



        [Display(Name = ("Primer apellido"))]
        [MaxLength(50)]
        public string per_primer_ape { get; set; }

        [Display(Name = ("Segundo apellido"))]
        [MaxLength(50)]
        public string per_segundo_ape { get; set; }


        #endregion Modelo Personas
    }
}
