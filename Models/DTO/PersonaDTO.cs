﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Migrantes.Models.DTO
{
    public class PersonaDTO
    {
        public int per_codigo_id { get; set; }

        [Required(ErrorMessage = "Primer apellido es campo requerido")]
        [Display(Name = ("Primer apellido *"))]
        [MaxLength(50)]
        [DisplayName("Primer apellido")]
        public string per_primer_ape { get; set; }

        [DisplayName("Segundo apellido")]
        [Display(Name = ("Segundo apellido"))]
        [MaxLength(50)]
        public string per_segundo_ape { get; set; }

        [DisplayName("Apellido de casada")]
        [Display(Name = ("Apellido de casada"))]
        [MaxLength(50)]
        public string per_apellido_cas { get; set; }

        [DisplayName("Primer nombre")]
        [Required(ErrorMessage = "Primer nombre es campo requerido")]
        [Display(Name = ("Primer nombre *"))]
        [MaxLength(50)]
        public string per_primer_nom { get; set; }

        [DisplayName("Segundo nombre")]
        [Display(Name = ("Segundo nombre"))]
        [MaxLength(50)]
        public string per_segundo_nom { get; set; }

        [DisplayName("Letra índice")]
        [Display(Name = ("Letra índice"))]
        [MaxLength(50)]
        public string per_letra_indice { get; set; }

        [DisplayName("Otros nombres")]
        [Display(Name = ("Otros nombres"))]
        [MaxLength(50)]
        public string per_otros_nom { get; set; }

        [DisplayName("Edad")]
        [Display(Name = ("Edad"))]
        public int per_edad { get; set; }

        [DisplayName("Edad")]
        [Display(Name = ("Edad"))]
        public string Edad { get; set; }

        [DisplayName("Nombre usual")]
        [Display(Name = ("Nombre usual"))]
        [MaxLength(250)]
        public string per_nombre_usual { get; set; }

        [Required(ErrorMessage = "Sexo es campo requerido")]
        [Display(Name = ("Sexo *"))]
        public int per_sexo { get; set; }

        [DisplayName("Fecha de nacimiento")]
        [Display(Name = ("Fecha de nacimiento *"))]
        [Required(ErrorMessage = "Fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime per_fecha_nac { get; set; }

        [Required(ErrorMessage = "Profesión es campo requerido")]
        [DisplayName("Profesión")]
        [Display(Name = ("Profesión *"))]
        [MaxLength(100)]
        public string per_profesion { get; set; }

        [Required(ErrorMessage = "Estado civil es campo requerido")]
        [Display(Name = ("Estado civil *"))]
        public int per_estado_civil { get; set; }

        [DisplayName("Correo electrónico")]
        [Display(Name = ("Correo electrónico"))]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe ser formato de correo")]
        public string per_email { get; set; }

        [DisplayName("Correo electrónico alterno")]
        [Display(Name = ("Correo electrónico alterno"))]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe ser formato de correo")]
        public string per_email_interno { get; set; }

        [DisplayName("Número de teléfono")]
        [Display(Name = ("Número de teléfono *"))]
        [Required(ErrorMessage = "Número de teléfono es campo requerido")]
        public string per_telefono_movil { get; set; }

        [DisplayName("Teléfono alterno")]
        [Display(Name = ("Teléfono alterno"))]
        [MaxLength(20)]
        public string per_telefono_interno { get; set; }

        [DisplayName("Observaciones")]
        [Display(Name = ("Observaciones"))]
        [MaxLength(4000)]
        public string per_observaciones { get; set; }

        [DisplayName("Sexo")]
        [Display(Name = ("Sexo"))]
        public string Sexo { get; set; }

        [DisplayName("Estado civil")]
        [Display(Name = ("Estado civil"))]
        public string EstadoCivil { get; set; }

        [DisplayName("Nacionalidad")]
        [Display(Name = ("Nacionalidad"))]
        public string per_codpai_nacionalidad { get; set; }

        [DisplayName("País")]
        [Display(Name = ("País"))]
        public string per_codpai_nacimiento { get; set; }

        [DisplayName("Usuario grabación")]
        [Display(Name = ("Usuario grabación"))]
        public string per_usuario_grabacion { get; set; }

        [DisplayName("Fecha grabación")]
        [Display(Name = ("Fecha grabación"))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime per_fecha_grabacion { get; set; }

        [DisplayName("Usuario modificación")]
        [Display(Name = ("Usuario modificación"))]
        public string per_usuario_modificacion { get; set; }

        [DisplayName("Fecha modificación")]
        [Display(Name = ("Fecha modificación"))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime per_fecha_modificacion { get; set; }
    }
}
