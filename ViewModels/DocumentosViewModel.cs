using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Migrantes.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migrantes.ViewModels
{
    public class DocumentosViewModel
    {

        public int ide_id_persona { get; set; }

        public int ide_codigo_id { get; set; }


        [Display(Name = ("Tipo de Documento"))]
        [Required(ErrorMessage = "Seleccione un tipo de documento por favor...")]
        [ForeignKey("TipoDocumento")]
        public int ide_id_documento { get; set; }

        [MaxLength(100)]
        public string tid_descripcion { get; set; }

        [Display(Name = ("Número de DPI, Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Introduce el número del documento")]
        public int ide_numero { get; set; }


        [Display(Name = ("Fecha emisión del DPI, Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Elige la fecha de emisión del documento")]
        [DataType(DataType.Date)]
        public DateTime ide_fecha_emision { get; set; }


        [Display(Name = ("Fecha vencimiento del DPI, Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Elige la fecha de vencimiento del documento")]
        [DataType(DataType.Date)]
        public DateTime ide_fecha_vencimiento { get; set; }


        [MaxLength(150)]
        public string NombreImagenPortada_A { get; set; }


        [MaxLength(250)]
        public string RutaImagenPortada_A { get; set; }



        [MaxLength(150)]
        public string NombreImagenPortada_B { get; set; }


        [MaxLength(250)]
        public string RutaImagenPortada_B { get; set; }

        
        [Display(Name = ("Portada del documento"))]
        [NotMapped]
        public IFormFile FotoDocumento { get; set; }

        
        [Display(Name = ("Contraportada del documento"))]
        [NotMapped]
        public IFormFile FotoDocumento_LadoB { get; set; }


        public int ide_estado { get; set; }


        public bool ide_entregado { get; set; }



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



    }
}
