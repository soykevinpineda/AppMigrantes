using Microsoft.AspNetCore.Http;
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


        [Display(Name = ("Número de DPI, Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Número del documento es requerido")]
        public int ide_numero { get; set; }


        [Display(Name = ("Fecha emisión del DPI, Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Fecha de emision es requerida")]
        [DataType(DataType.Date)]
        public DateTime ide_fecha_emision { get; set; }


        [Display(Name = ("Fecha vencimiento del DPI, Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Fecha de vencimiento es requerida")]
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
        [Required(ErrorMessage = "Seleccione una foto por favor...")]
        [NotMapped]
        public IFormFile FotoDocumento { get; set; }

        [Display(Name = ("Contraportada del documento"))]
        [Required(ErrorMessage = "Seleccione una foto por favor...")]
        [NotMapped]
        public IFormFile FotoDocumento_LadoB { get; set; }


        public int ide_estado { get; set; }


        public bool ide_entregado { get; set; }

    }
}
