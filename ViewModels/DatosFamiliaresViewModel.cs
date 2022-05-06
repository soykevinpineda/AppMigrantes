
using Microsoft.AspNetCore.Http;
using Migrantes.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace Migrantes.ViewModels
{
    public class DatosFamiliaresViewModel
    {

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }


        [Display(Name = ("Nombres de la madre *"))]
        [Required(ErrorMessage = "Nombre es campo requerido")]
        public string nombres_madre { get; set; }

        [Display(Name = ("Primer apellido *"))]
        [Required(ErrorMessage = "Primer apellido es campo requerido")]
        public string primer_apellido_madre { get; set; }

        [Display(Name = ("Segundo apellido"))]
        public string segundo_apellido_madre { get; set; }

        [Display(Name = ("Edad"))]
        public int edad_madre { get; set; }

        [Display(Name = ("Profesión"))]
        public string profesion_madre { get; set; }




        [Display(Name = ("Nombres del padre *"))]
        [Required(ErrorMessage = "Nombre es campo requerido")]
        public string nombres_padre { get; set; }

        [Display(Name = ("Primer apellido *"))]
        [Required(ErrorMessage = "Primer apellido es campo requerido")]
        public string primer_apellido_padre { get; set; }

        [Display(Name = ("Segundo apellido"))]
        public string segundo_apellido_padre { get; set; }


        [Display(Name = ("Edad"))]
        public int edad_padre { get; set; }

        [Display(Name = ("Profesión"))]
        public string profesion_padre { get; set; }

        public int estado_datosfamiliares { get; set; }

    }
}
