using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Migrantes.Data;
using Migrantes.Data.Servicios.Documentos;
using Migrantes.Data.Servicios.Familiares;
using Migrantes.Data.Servicios.Fiador;
using Migrantes.Data.Servicios.Personas;
using Migrantes.Models.DTO;
using Migrantes.Models.Entities;
using Migrantes.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Migrantes.Controllers
{
    public class PersonasController : GenericController
    {
        //Aqui se van agregando las Interfaces creadas con -private readonly.
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        private readonly IMapper _imapper;
        private readonly IDocumentos _documentos;
        private readonly IPersonas _persona;
        private readonly IFamiliares _familiares;
        public const string RutaImagen1 = @"\DocumentosAdjuntos\DocsLadoA\";
        public const string RutaImagen2 = @"\DocumentosAdjuntos\DocsLadoB\";



        //Aqui se van agregando las Interfaces creadas.
        public PersonasController(ApplicationDbContext context,
            IWebHostEnvironment IWebHostEnvironment, IMapper imapper,
            IDocumentos documentos, IPersonas persona, IFamiliares agregandofamiliares)
        {
            
            this._IWebHostEnvironment = IWebHostEnvironment;
            this._imapper = imapper;
            this._documentos = documentos;
            this._persona = persona;
            this._familiares = agregandofamiliares;
            this._context = context;
        }

        //public static List<PersonaDTO> oPersonasExcel;


        //public FileResult ExportarPersonasExcel(string[] nombrePropiedades)
        //{
        //    byte[] buffer = ExportarExcelGeneric(nombrePropiedades, oPersonasExcel);
        //    return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //}



        #region Area personas

        public async Task<IActionResult> Index()
        {

            var urlRetorno1 = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno1", urlRetorno1);

            return View(await _context.PersonasDb.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await this._context.PersonasDb
                .FirstOrDefaultAsync(m => m.per_codigo_id == id);

            var PersonaDoc = this._context.PersonasDb.FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonaDoc.per_codigo_id;

            DocPersona(PersonaDoc.per_codigo_id);

            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }


        public IActionResult Create()
        {
            return View();
        }


        //GET CREAR PERSONA
        public ActionResult CrearPersona()
        {


            Sexos();
            EstadoCivil();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("per_codigo_id,per_estado,per_codigo_alternativo," +
            "per_letra_indice,per_primer_ape,per_segundo_ape,per_apellido_cas,per_primer_nom,per_segundo_nom," +
            "per_otros_nom,per_nombre_usual,per_codpai_nacionalidad,per_codpai_nacimiento,per_sexo,per_edad," +
            "per_fecha_nac,per_profesion,per_estado_civil,per_email,per_codmun_nac,per_coddep_nac,per_lugar_nac," +
            "per_email_interno,per_telefono_movil,per_telefono_interno,per_apellidos_nombres,per_observaciones," +
            "per_codpai_digita,per_usuario_grabacion,per_fecha_grabacion,per_usuario_modificacion," +
            "per_fecha_modificacion")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                this._context.Add(persona);
                await this._context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(persona);
        }


        //POST CREAR PERSONA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuardarPersona(PersonaDTO oPersona)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("CrearPersona", "Personas");
                }
                else
                {
                    await this._persona.GuardarPersona(oPersona);
                }
            }
            catch (Exception)
            {
                throw;
            }


            return RedirectToAction("Personas", "Personas");
        }

        private bool PersonaExists(int id)
        {
            return this._context.PersonasDb.Any(e => e.per_codigo_id == id);
        }


        //GET Edit 
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.EstCivilEditar = EstadoCivilEditar();
            ViewBag.SexoEditar = SexosEditar();

            var documento = this._context.PersonasDb.FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = documento.per_codigo_id;

            //Obtenemos el ID del documento asociado a la persona.

            var oDocumento = this._context.IdentidadPersonasDb.FirstOrDefault(x => x.ide_codigo_id == id);
            ViewBag.IdDocumento = oDocumento;


            DocPersona(documento.per_codigo_id);

            if (id == null)
            {
                return NotFound();
            }

            var persona = await this._context.PersonasDb.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }


            if (oDocumento == null)
            {
                TempData["msj"] = "La persona no tiene documentos agregados...";
                return View(persona);
            }

            return View(persona);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("per_codigo_id,per_estado,per_codigo_alternativo," +
            "per_letra_indice,per_primer_ape,per_segundo_ape,per_apellido_cas,per_primer_nom,per_segundo_nom," +
            "per_otros_nom,per_nombre_usual,per_codpai_nacionalidad,per_codpai_nacimiento,per_sexo,per_edad," +
            "per_fecha_nac,per_profesion,per_estado_civil,per_email,per_codmun_nac,per_coddep_nac,per_lugar_nac," +
            "per_email_interno,per_telefono_movil,per_telefono_interno,per_apellidos_nombres,per_observaciones," +
            "per_codpai_digita,per_usuario_grabacion,per_fecha_grabacion,per_usuario_modificacion," +
            "per_fecha_modificacion")] Persona persona)
        {

            DocPersona(persona.per_codigo_id);

            if (id != persona.per_codigo_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var edad = DateTime.Now.Year - persona.per_fecha_nac.Year;
                    persona.per_edad = edad;
                    persona.per_usuario_modificacion = "COMERCIAL";
                    persona.per_fecha_modificacion = DateTime.Now;
                    this._context.Update(persona);
                    await this._context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.per_codigo_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Personas", "Personas");
            }

            return View(persona);
        }



        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            var persona = await this._context.PersonasDb.FirstOrDefaultAsync(m => m.per_codigo_id == id);

            if (id == null)
            {
                return NotFound();
            }


            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await this._context.PersonasDb.FindAsync(id);
            this._context.PersonasDb.Remove(persona);
            await this._context.SaveChangesAsync();

            return RedirectToAction("Personas", "Personas");
        }

        #endregion Area de Personas


        #region Procedimientos Sexo / Estado Civil
        //Combo Box: Tipo Sexo
        public void Sexos()
        {

            List<SelectListItem> oSexo = new List<SelectListItem>();

            try
            {
                oSexo = (from s in this._context.SexosDb
                         where s.activo == 1
                         select new SelectListItem
                         {
                             Text = s.nombre_sexo,
                             Value = s.id_sexo.ToString()
                         }).ToList();
                oSexo.Insert(0, new SelectListItem
                {
                    Text = "--Seleccione--",
                    Value = ""
                });
            }
            catch (Exception)
            {
                throw;
            }

            ViewBag.Sexos = oSexo;

        }

        //Lista para mostrar en la vista Editar
        public List<SelectListItem> SexosEditar()
        {
            List<SelectListItem> oSexo = new List<SelectListItem>();//Combo Box

            try
            {
                oSexo = (from s in this._context.SexosDb
                         where s.activo == 1
                         select new SelectListItem
                         {
                             Text = s.nombre_sexo,
                             Value = s.id_sexo.ToString()
                         }).ToList();
                oSexo.Insert(0, new SelectListItem
                {
                    Text = "--Seleccione--",
                    Value = ""
                });
            }
            catch (Exception)
            {
                throw;
            }

            return oSexo;
        }


        //Combo Box: Estado Civil
        public void EstadoCivil()
        {
            List<SelectListItem> oEstado = new List<SelectListItem>();//Combo Box

            try
            {
                oEstado = (from e in this._context.EstadoCivilDb
                           where e.activo == 1
                           select new SelectListItem
                           {
                               Text = e.estado_civil,
                               Value = e.id_estado_civil.ToString()
                           }).ToList();
                oEstado.Insert(0, new SelectListItem
                {
                    Text = "--Seleccione--",
                    Value = ""
                });
            }
            catch (Exception)
            {
                throw;
            }

            ViewBag.EstadoCivil = oEstado;
        }


        //Lista para mostrar en la vista Editar
        public List<SelectListItem> EstadoCivilEditar()
        {
            List<SelectListItem> oEstadoCivilEditar = new List<SelectListItem>();

            try
            {
                oEstadoCivilEditar = (from s in this._context.EstadoCivilDb
                                      where s.activo == 1
                                      select new SelectListItem
                                      {
                                          Text = s.estado_civil,
                                          Value = s.id_estado_civil.ToString()
                                      }).ToList();
                oEstadoCivilEditar.Insert(0, new SelectListItem
                {
                    Text = "--Seleccione--",
                    Value = ""
                });
            }
            catch (Exception)
            {
                throw;
            }

            return oEstadoCivilEditar;
        }

        #endregion


        #region Método crea una lista de documentos disponibles de la persona
        public void DocPersona(int idPersona)

        {
            List<DocumentosPersonaDTO> ListDocumentos = new List<DocumentosPersonaDTO>();

            ListDocumentos = (from identidad in this._context.IdentidadPersonasDb
                              join persona in this._context.PersonasDb
                              on identidad.ide_codigo_id equals persona.per_codigo_id
                              join TipoDoc in this._context.TipoDocumentosDb
                              on identidad.ide_id_documento equals TipoDoc.tid_id_documento
                              where persona.per_codigo_id == idPersona


                              select new DocumentosPersonaDTO

                              {
                                  RutaImagenPortada_A = identidad.RutaImagenPortada_A,
                                  NombreImagenPortada_A = identidad.NombreImagenPortada_A,
                                  RutaImagenPortada_B = identidad.RutaImagenPortada_B,
                                  NombreImagenPortada_B = identidad.NombreImagenPortada_B,
                                  ide_id_persona = identidad.ide_id_persona,
                                  NombreDocumento = TipoDoc.tid_descripcion,
                                  per_codigo_id = persona.per_codigo_id,
                                  per_primer_nom = persona.per_primer_nom,
                                  per_primer_ape = persona.per_primer_ape,
                                  ide_id_documento = identidad.ide_id_documento,
                                  ide_numero = identidad.ide_numero,
                                  ide_fecha_emision = identidad.ide_fecha_emision,
                                  ide_fecha_vencimiento = identidad.ide_fecha_vencimiento

                              }).ToList();

            ViewBag.ListadoDocumentos = ListDocumentos;

        }
        #endregion



        #region Area del buscador de personas
        public ActionResult Personas(string oBuscador)
        {
            ViewData["Buscador"] = oBuscador;

            List<PersonaDTO> oPersonas = new List<PersonaDTO>();

            if (String.IsNullOrEmpty(oBuscador))
            {
                oPersonas = (from p in this._context.PersonasDb
                             join s in this._context.SexosDb
                             on p.per_sexo equals s.id_sexo
                             join e in this._context.EstadoCivilDb
                             on p.per_estado_civil equals e.id_estado_civil
                             select new PersonaDTO
                             {
                                 per_codigo_id = p.per_codigo_id,
                                 per_primer_ape = p.per_primer_ape,
                                 per_segundo_ape = p.per_segundo_ape,
                                 per_apellido_cas = p.per_apellido_cas,
                                 per_primer_nom = p.per_primer_nom,
                                 per_segundo_nom = p.per_segundo_nom,
                                 per_otros_nom = p.per_otros_nom,
                                 per_nombre_usual = p.per_nombre_usual,
                                 Sexo = s.nomenclatura_sexo,
                                 per_fecha_nac = p.per_fecha_nac,
                                 per_profesion = p.per_profesion,
                                 EstadoCivil = e.estado_civil,
                                 per_email = p.per_email,
                                 per_email_interno = p.per_email_interno,
                                 per_telefono_movil = p.per_telefono_movil,
                                 per_telefono_interno = p.per_telefono_interno,
                                 per_observaciones = p.per_observaciones,
                                 per_codpai_nacionalidad = p.per_codpai_nacionalidad,
                                 per_codpai_nacimiento = p.per_codpai_nacimiento,
                                 Edad = Convert.ToString(p.per_edad),
                                 per_fecha_grabacion = p.per_fecha_grabacion,
                                 per_usuario_grabacion = p.per_usuario_grabacion,
                                 per_letra_indice = p.per_letra_indice,
                                 per_usuario_modificacion = p.per_usuario_modificacion,
                                 per_fecha_modificacion = p.per_fecha_modificacion,
                                 per_edad = p.per_edad

                             }).OrderByDescending(x => x.per_fecha_grabacion).ToList();

                if (oPersonas.Count() == 0)
                {
                    ViewData["TotalPersonas"] = oPersonas.Count().ToString();
                    ViewData["Validacion"] = 0;
                }
                else
                {
                    ViewData["TotalPersonas"] = oPersonas.Count().ToString();
                    ViewData["Validacion"] = 1;
                }
            }
            else
            {
                oPersonas = (from p in this._context.PersonasDb
                             join s in this._context.SexosDb
                             on p.per_sexo equals s.id_sexo
                             join e in this._context.EstadoCivilDb
                             on p.per_estado_civil equals e.id_estado_civil
                             select new PersonaDTO
                             {
                                 per_codigo_id = p.per_codigo_id,
                                 per_primer_ape = p.per_primer_ape,
                                 per_segundo_ape = p.per_segundo_ape,
                                 per_apellido_cas = p.per_apellido_cas,
                                 per_primer_nom = p.per_primer_nom,
                                 per_segundo_nom = p.per_segundo_nom,
                                 per_otros_nom = p.per_otros_nom,
                                 per_nombre_usual = p.per_nombre_usual,
                                 Sexo = s.nomenclatura_sexo,
                                 per_fecha_nac = p.per_fecha_nac,
                                 per_profesion = p.per_profesion,
                                 EstadoCivil = e.estado_civil,
                                 per_email = p.per_email,
                                 per_email_interno = p.per_email_interno,
                                 per_telefono_movil = p.per_telefono_movil,
                                 per_telefono_interno = p.per_telefono_interno,
                                 per_observaciones = p.per_observaciones,
                                 per_codpai_nacionalidad = p.per_codpai_nacionalidad,
                                 per_codpai_nacimiento = p.per_codpai_nacimiento,
                                 Edad = Convert.ToString(p.per_edad),
                                 per_fecha_grabacion = p.per_fecha_grabacion,
                                 per_usuario_grabacion = p.per_usuario_grabacion,
                                 per_usuario_modificacion = p.per_usuario_modificacion,
                                 per_fecha_modificacion = p.per_fecha_modificacion,
                                 per_edad = p.per_edad

                             }).Where(x => x.per_primer_nom.Contains(oBuscador)
                                     || x.per_segundo_nom.Contains(oBuscador)
                                     || x.per_primer_ape.Contains(oBuscador)
                                     || x.per_segundo_ape.Contains(oBuscador)
                                     || x.per_apellido_cas.Contains(oBuscador))
                               .OrderByDescending(x => x.per_fecha_grabacion).ToList();

                if (oPersonas.Count() == 0)
                {
                    ViewData["TotalPersonas"] = oPersonas.Count().ToString();
                    ViewData["Validacion"] = 3;
                }
                else
                {
                    ViewData["TotalPersonas"] = oPersonas.Count().ToString();
                    ViewData["Validacion"] = 2;
                }
            }

            oPersonasExcel = oPersonas;
            return View(oPersonas);
        }
        #endregion Area del Buscador de Personas


#pragma warning disable CA2211
    }

}
