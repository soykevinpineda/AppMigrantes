using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Migrantes.Models.Entities
{
    public class Persona
    {
        [Key]
        public int per_codigo_id { get; set; }


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


        [Display(Name = ("Apellido de casada"))]
        [MaxLength(50)]
        public string per_apellido_cas { get; set; }


        [Display(Name = ("Fecha nacimiento"))]
        [DataType(DataType.Date)]
        public DateTime per_fecha_nac { get; set; }


        [Display(Name = ("Edad"))]
        public int per_edad { get; set; }


        [Display(Name = ("Sexo"))]
        public int per_sexo { get; set; }


        [Display(Name = ("País de nacimiento"))]
        [MaxLength(2)]
        public string per_codpai_nacimiento { get; set; }


        [Display(Name = ("Nacionalidad"))]
        [MaxLength(2)]
        public string per_codpai_nacionalidad { get; set; }


        [Display(Name = ("Profesión"))]
        [MaxLength(100)]
        public string per_profesion { get; set; }


        [Display(Name = ("Estado civil"))]
        public int per_estado_civil { get; set; }


        [Display(Name = ("Email"))]
        [MaxLength(100)]
        public string per_email { get; set; }


        [Display(Name = ("Código municipio"))]
        public int per_codmun_nac { get; set; }

        [Display(Name = ("Código departamento"))]
        public int per_coddep_nac { get; set; }

        [Display(Name = ("Código lugar de nacimiento"))]
        public int per_lugar_nac { get; set; }

        [Display(Name = ("Email alterno"))]
        [MaxLength(100)]
        public string per_email_interno { get; set; }

        [Display(Name = ("Teléfono"))]
        [MaxLength(20)]
        public string per_telefono_movil { get; set; }

        [Display(Name = ("Teléfono alterno"))]
        [MaxLength(20)]
        public string per_telefono_interno { get; set; }

        [Display(Name = ("Observaciones"))]
        [MaxLength(4000)]
        public string per_observaciones { get; set; }


        [Display(Name = ("Estado"))]
        public int per_estado { get; set; }

        [Display(Name = ("Código alternativo EVO"))]
        [MaxLength(50)]
        public string per_codigo_alternativo { get; set; }

        [Display(Name = ("Letra índice"))]
        [MaxLength(50)]
        public string per_letra_indice { get; set; }


        [Display(Name = ("Código país"))]
        [MaxLength(2)]
        public string per_codpai_digita { get; set; }

        [Display(Name = ("Usuario grabación"))]
        [MaxLength(50)]
        public string per_usuario_grabacion { get; set; }

        [Display(Name = ("Fecha grabación"))]
        [DataType(DataType.DateTime)]
        public DateTime per_fecha_grabacion { get; set; }

        [Display(Name = ("Usuario modificación"))]
        [MaxLength(50)]
        public string per_usuario_modificacion { get; set; }

        [Display(Name = ("Fecha modificación"))]
        [DataType(DataType.DateTime)]
        public DateTime per_fecha_modificacion { get; set; }

        public virtual ICollection<IdentidadPersona> IdentidadPersonaLink { get; set; }

        public virtual ICollection<DatosFamiliares> DatosFamiliaresLink { get; set; }

        public virtual ModeloFiador ModeloFiador { get; set; }
    }
}
