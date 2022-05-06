using Migrantes.Models.DTO;
using Migrantes.Models.Entities;
using Migrantes.ViewModels;
using System;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Personas
{
    public class Personas : IPersonas
    {

        private readonly ApplicationDbContext _context;

        public Personas(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task GuardarPersona(PersonaDTO oPersona)
        {

            Persona oPersonaDb = new Persona();

            using (var db = new ApplicationDbContext())

            {
                oPersonaDb.per_primer_ape = oPersona.per_primer_ape;
                if (oPersona.per_segundo_ape == null)
                {
                    oPersonaDb.per_segundo_ape = "N/A";
                }
                else
                {
                    oPersonaDb.per_segundo_ape = oPersona.per_segundo_ape;
                }
                if (oPersona.per_apellido_cas == null)
                {
                    oPersonaDb.per_apellido_cas = "N/A";
                }
                else
                {
                    oPersonaDb.per_apellido_cas = oPersona.per_apellido_cas;
                }
                oPersonaDb.per_primer_nom = oPersona.per_primer_nom;
                if (oPersona.per_segundo_nom == null)
                {
                    oPersonaDb.per_segundo_nom = "N/A";
                }
                else
                {
                    oPersonaDb.per_segundo_nom = oPersona.per_segundo_nom;
                }
                if (oPersona.per_otros_nom == null)
                {
                    oPersonaDb.per_otros_nom = "N/A";
                }
                else
                {
                    oPersonaDb.per_otros_nom = oPersona.per_otros_nom;
                }
                if (oPersona.per_nombre_usual == null)
                {
                    oPersonaDb.per_nombre_usual = "N/A";
                }
                else
                {
                    oPersonaDb.per_nombre_usual = oPersona.per_nombre_usual;
                }
                oPersonaDb.per_sexo = oPersona.per_sexo;
                oPersonaDb.per_fecha_nac = oPersona.per_fecha_nac;
                var edad = DateTime.Now.Year - oPersona.per_fecha_nac.Year;
                oPersonaDb.per_edad = edad;
                oPersonaDb.per_profesion = oPersona.per_profesion;
                oPersonaDb.per_estado_civil = oPersona.per_estado_civil;
                if (oPersona.per_email == null)
                {
                    oPersonaDb.per_email = "N/A";
                }
                else
                {
                    oPersonaDb.per_email = oPersona.per_email;
                }
                if (oPersona.per_email_interno == null)
                {
                    oPersonaDb.per_email_interno = "N/A";
                }
                else
                {
                    oPersonaDb.per_email_interno = oPersona.per_email_interno;
                }
                oPersonaDb.per_telefono_movil = oPersona.per_telefono_movil;
                if (oPersona.per_telefono_interno == null)
                {
                    oPersonaDb.per_telefono_interno = "N/A";
                }
                else
                {
                    oPersonaDb.per_telefono_interno = oPersona.per_telefono_interno;
                }
                oPersonaDb.per_apellidos_nombres = "N/A";
                if (oPersona.per_observaciones == null)
                {
                    oPersonaDb.per_observaciones = "N/A";
                }
                else
                {
                    oPersonaDb.per_observaciones = oPersona.per_observaciones;
                }
                oPersonaDb.per_codpai_nacionalidad = "GT";
                oPersonaDb.per_codpai_nacimiento = "GT";
                oPersonaDb.per_codpai_digita = "GT";
                oPersonaDb.per_usuario_grabacion = "COMERCIAL";
                oPersonaDb.per_fecha_grabacion = DateTime.Now;
                oPersonaDb.per_estado = 1;
                oPersonaDb.per_codigo_alternativo = "N/A";
                oPersonaDb.per_letra_indice = "N/A";
                oPersonaDb.per_usuario_modificacion = "N/A";


                //se utiliza siempre "await" al iniciar conexion a la base de datos.
                await this._context.AddAsync(oPersonaDb);
                await this._context.SaveChangesAsync();


            }

        }
    }
}
