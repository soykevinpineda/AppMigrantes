using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migrantes.Models.DTO
{
    public class DatosFamiliaresDTO
    {

        [Key]
        public int DatosFamiliaresID { get; set; }

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }


        [Display(Name = ("Parientes"))]
        [Required(ErrorMessage = "Seleccione un pariente")]
        [ForeignKey("Parientes")]
        public int ParienteID { get; set; }


        public string NombreDelPariente { get; set; }


        [Display(Name = ("Primer nombre"))]
        [Required(ErrorMessage = "Introduce el primer nombre")]
        public string PrimerNombreFamiliar { get; set; }

        [Display(Name = ("Segundo nombre"))]
        [Required(ErrorMessage = "Introduce el segundo nombre")]
        public string SegundoNombreFamiliar { get; set; }

        [Display(Name = ("Apellidos"))]
        [Required(ErrorMessage = "Introduce los apellidos")]
        public string ApellidosFamiliar { get; set; }


        [Display(Name = ("Fecha de nacimiento"))]
        [Required(ErrorMessage = "Elige una fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoDelFamiliar { get; set; }

        [Display(Name = ("País de nacimiento"))]
        [Required(ErrorMessage = "Introduce un país de nacimiento")]
        [MaxLength(50)]
        public string PaisNacimientoDelFamiliar { get; set; }

        [Display(Name = ("Edad"))]
        public int EdadDelFamiliar { get; set; }

        [Display(Name = ("Profesión"))]
        [MaxLength(75)]
        public string ProfesionDelFamiliar { get; set; }

        [Display(Name = ("Teléfono"))]
        [Required(ErrorMessage = "Introduce un número teléfonico")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(25)]
        public string TelefonoDelFamiliar { get; set; }

        [Display(Name = ("Teléfono alternativo"))]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(25)]
        public string TelefonoAlternativoFamiliar { get; set; }

        [Display(Name = ("E-mail"))]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string EmaiDelFamiliar { get; set; }

        public int EstadoDatosFamiliares { get; set; }

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
