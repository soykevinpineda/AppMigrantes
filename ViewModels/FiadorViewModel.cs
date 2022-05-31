using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migrantes.ViewModels
{
    public class FiadorViewModel
    {
        [Key]
        public int Id_Fiador { get; set; }

        [ForeignKey("Persona")]
        public int per_codigo_id { get; set; }


        [Display(Name = ("Primer nombre"))]
        public string Primer_nombre_Fiador { get; set; }

        [Display(Name = ("Segundo nombre"))]
        public string Segundo_nombre_Fiador { get; set; }

        [Display(Name = ("Primer apellido"))]
        public string Primer_apellido_Fiador { get; set; }


        [Display(Name = ("Segundo apellido"))]
        public string Segundo_apellido_Fiador { get; set; }

        [Display(Name = ("País de nacimiento"))]
        public string Pais_nacimiento_Fiador { get; set; }

        [Display(Name = ("Sexo"))]
        public int Sexo_Fiador { get; set; }

        [Display(Name = ("Edad"))]
        public int Edad_Fiador { get; set; }

        [Display(Name = ("Email"))]
        public string Email_Fiador { get; set; }

        [Display(Name = ("Teléfono movil"))]
        public int Telefono_Fiador { get; set; }

        [Display(Name = ("Teléfono movil alternativo"))]
        public int Telefono_alterno_Fiador { get; set; }


        [Display(Name = ("Nro. Cartas de recomendación personal"))]
        public int Num_cartas_personales { get; set; }


        [Display(Name = ("Nro. Cartas de recomendación familiar"))]
        public int Num_cartas_familiares { get; set; }

        [Display(Name = ("Entrego recibo de agua o luz?"))]
        public bool Entrego_recibo_agua_o_luz { get; set; }


        [Display(Name = ("Fecha de grabación"))]
        [DataType(DataType.DateTime)]
        public DateTime Fecha_grabacion_Fiador { get; set; } = DateTime.Now;


    }
}
