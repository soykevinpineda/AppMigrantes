using Microsoft.AspNetCore.Http;
using Migrantes.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Migrantes.Models.DTO
{
    public class DocumentosPersonaDTO
    {

        public int ide_codigo_id { get; set; }

        public int ide_id_persona { get; set; }

        public int per_codigo_id { get; set; }

        [MaxLength(100)]
        public string tid_descripcion { get; set; }



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

        [Display(Name = ("Fecha grabación"))]
        [DataType(DataType.DateTime)]
        public DateTime per_fecha_grabacion { get; set; }


        [Display(Name = ("Usuario modificación"))]
        [MaxLength(50)]
        public string per_usuario_modificacion { get; set; }


        [Display(Name = ("Fecha modificación"))]
        [DataType(DataType.DateTime)]
        public DateTime per_fecha_modificacion { get; set; }



        #region Tipo de Documento


        [Display(Name = ("Nombre Documento"))]
        public string NombreDocumento { get; set; }


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


        [Display(Name = ("Tipo de Documento"))]
        [Required(ErrorMessage = "Seleccione un tipo de documento por favor...")]
        [ForeignKey("TipoDocumento")]
        public int ide_id_documento { get; set; }


        [Display(Name = ("Número de DPI,Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Número del documento es requerido")]
        public int ide_numero { get; set; }


        [Display(Name = ("Fecha emisión del DPI,Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Fecha de emision es requerida")]
        [DataType(DataType.Date)]
        public DateTime ide_fecha_emision { get; set; }


        [Display(Name = ("Fecha vencimiento del DPI,Pasaporte o Seguro Social"))]
        [Required(ErrorMessage = "Fecha de vencimiento es requerida")]
        [DataType(DataType.Date)]
        public DateTime ide_fecha_vencimiento { get; set; }


        #endregion TipoDocumento


        #region Datos de Familiares

        [Key]
        public int Id_DatosFamiliares { get; set; }

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

        public static implicit operator List<object>(DocumentosPersonaDTO v)
        {
            throw new NotImplementedException();
        }

        #endregion Datos de Familiares



    }
}
