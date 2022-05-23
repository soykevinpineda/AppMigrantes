﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Migrantes.Data;
using Migrantes.Data.Servicios.Documentos;
using Migrantes.Data.Servicios.Familiares;
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
    public class DocumentosController : GenericController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        private readonly IDocumentos _documentos;
        private readonly IPersonas _persona;
        private readonly IFamiliares _familiares;
        public const string RutaImagen1 = @"\DocumentosAdjuntos\DocsLadoA\";
        public const string RutaImagen2 = @"\DocumentosAdjuntos\DocsLadoB\";



        //Aqui se van agregando las Interfaces creadas.
        public DocumentosController(ApplicationDbContext context,
            IWebHostEnvironment IWebHostEnvironment,
            IDocumentos documentos, IPersonas persona, IFamiliares agregandofamiliares)
        {
            this._IWebHostEnvironment = IWebHostEnvironment;
            this._documentos = documentos;
            this._persona = persona;
            this._familiares = agregandofamiliares;
            this._context = context;
        }

        #region Area documentos de la persona


        //Método crea una lista
        //de los tipos de documentos guardados en la DB.
        public void TipoDoc(int id)
        {
            //Guardamos en una variable
            //para seleccionar de la DB el tipo de documento.
            var TipoDocumentos = this._context.IdentidadPersonasDb
                .Where(x => x.ide_codigo_id == id)
                .Select(x => x.ide_id_documento).ToList();


            ViewBag.ValidarDoc = TipoDocumentos;

            //Crea un ComboBox de tipo de documentos
            List<SelectListItem> oDoc = new List<SelectListItem>();

            try
            {
                oDoc = (from e in this._context.TipoDocumentosDb
                        where e.tid_activo == 1
                        && !TipoDocumentos.Contains(e.tid_id_documento)

                        select new SelectListItem
                        {
                            Text = e.tid_descripcion,
                            Value = e.tid_id_documento.ToString()
                        }).ToList();

                oDoc.Insert(0, new SelectListItem
                {

                    Text = "--Seleccione--",
                    Value = ""
                });
            }

            catch (Exception)
            {
                throw;
            }

            if (oDoc.Count == 1)

            {
                TempData["alerta"] = "Ya tiene todos sus documentos agregados!";
            }


            ViewBag.ListDoc = oDoc;
        }


        //GET: Creamos Documento de la persona
        [HttpGet]
        public IActionResult CrearDocumento(int id)


        {
            var DocumentoPersona = this._context.PersonasDb
                .FirstOrDefault(x => x.per_codigo_id == id);

            ViewBag.IdPersona = DocumentoPersona.per_codigo_id;

            TipoDoc(DocumentoPersona.per_codigo_id);

            return View();
        }

        //POST: Guardamos el documento asociado al id de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuardarDoc(IdentidadPersona identidad)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //Guardando Imagenes de Documentos: Lado A 

                    string wwwRothPath = this._IWebHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(identidad.FotoDocumento.FileName);
                    string extension = Path.GetExtension(identidad.FotoDocumento.FileName);
                    identidad.NombreImagenPortada_A = fileName = fileName + DateTime.Now.ToString("_ddMMyyyy-HHmmssffff") + extension;
                    string path2 = Path.Combine(RutaImagen1 + fileName);
                    string path = Path.Combine(wwwRothPath + "/DocumentosAdjuntos/DocsLadoA/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        identidad.FotoDocumento.CopyTo(fileStream);
                    }

                    //Guardando Imagenes de Documentos: Lado B

                    string wwwRothPathB = this._IWebHostEnvironment.WebRootPath;
                    string fileNameB = Path.GetFileNameWithoutExtension(identidad.FotoDocumento_LadoB.FileName);
                    string extensionB = Path.GetExtension(identidad.FotoDocumento_LadoB.FileName);
                    identidad.NombreImagenPortada_B = fileNameB = fileNameB + DateTime.Now.ToString("_ddMMyyyy-HHmmssffff") + extension;
                    string path2B = Path.Combine(RutaImagen2 + fileNameB);
                    string pathB = Path.Combine(wwwRothPathB + "/DocumentosAdjuntos/DocsLadoB/", fileNameB);


                    using (var fileStream = new FileStream(pathB, FileMode.Create))
                    {

                        identidad.FotoDocumento_LadoB.CopyTo(fileStream);
                    }

                    await this._documentos.GuardarDocumento(identidad, fileName, path2, fileNameB, path2B);
                }

                catch (Exception)
                {
                    throw;
                }
            }

            var urlRetornoDocs = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDocs);
        }


        //GET: Detalles del documento de la persona
        [HttpGet]
        public async Task<IActionResult> DetallesDocumento(int? id)
        {

            var documento = await this._context.IdentidadPersonasDb
                .FirstOrDefaultAsync(x => x.ide_id_persona == id);

            TipoDoc(documento.ide_id_documento);

            if (id == null)
            {
                return NotFound();
            }


            if (documento == null)
            {
                return NotFound();
            }

            //Declarando una instancia de ViewModel
            //a utilizar igualandolo al objeto de la Db
            var doc = new DocumentosViewModel()
            {
                ide_codigo_id = documento.ide_codigo_id,
                ide_id_persona = documento.ide_id_persona,
                ide_id_documento = documento.ide_id_documento,
                ide_numero = documento.ide_numero,
                ide_fecha_emision = documento.ide_fecha_emision,
                ide_fecha_vencimiento = documento.ide_fecha_vencimiento

            };

            return View(doc);
        }


        //GET: Editar documento de la persona
        [HttpGet]
        public async Task<IActionResult> EditarDocumento(int? id)
        {
            var documento = await this._context.IdentidadPersonasDb
                .FirstOrDefaultAsync(x => x.ide_id_persona == id);

            if (id == null)
            {
                return NotFound();
            }

            if (documento == null)
            {
                return NotFound();
            }

            //Declarando una instancia para usar un ViewModel
            //a utilizar igualandolo al objeto de la db
            var Doc = new DocumentosViewModel()
            {
                ide_id_persona = documento.ide_id_persona,
                ide_codigo_id = documento.ide_codigo_id,
                ide_id_documento = documento.ide_id_documento,
                ide_numero = documento.ide_numero,
                ide_fecha_emision = documento.ide_fecha_emision,
                ide_fecha_vencimiento = documento.ide_fecha_vencimiento,
                ide_estado = 1,
                ide_entregado = true,
            };

            TipoDoc(Doc.ide_id_documento);

            return View(Doc);
        }


        //POST: Guardar documento ya editado de la persona
        [HttpPost]
        public async Task<IActionResult> GuardarDocumentoEditado(DocumentosViewModel identidad)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await this._documentos.GuardarDocumentoEditado(identidad);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            var urlRetornoDocs = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDocs);
        }



        //GET: Eliminar documento de la persona
        [HttpGet]
        public async Task<IActionResult> EliminarDocumento(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var documento = await this._context.IdentidadPersonasDb
                .FirstOrDefaultAsync(x => x.ide_id_persona == id);

            TipoDoc(documento.ide_id_documento);

            if (documento == null)
            {
                return NotFound();
            }

            var Doc = new DocumentosViewModel()

            {
                ide_codigo_id = documento.ide_codigo_id,
                ide_id_persona = documento.ide_id_persona,
                ide_id_documento = documento.ide_id_documento,
                ide_numero = documento.ide_numero,
                ide_fecha_emision = documento.ide_fecha_emision,
                ide_fecha_vencimiento = documento.ide_fecha_vencimiento,
                ide_estado = 1,
                ide_entregado = true
            };

            return View(Doc);
        }


        //POST ELIMINAR DOCUMENTO
        [HttpPost]
        public async Task<IActionResult> EliminarDocumento(DocumentosViewModel IdentidadEliminada)
        {

            var documento = await this._context.IdentidadPersonasDb
                .AsNoTracking().FirstOrDefaultAsync(x => x.ide_id_persona == IdentidadEliminada.ide_id_persona);

            if (IdentidadEliminada == null)
            {
                return NotFound();

            }

            try
            {
                await this._documentos.EliminarDocumento(IdentidadEliminada);
            }
            catch (Exception)
            {
                throw;
            }


            var urlRetornoDocs = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDocs);
        }


        [HttpGet]
        public IActionResult DocumentosDisponibles(int? id)
        {
            //Obtenemos la ruta de inicio del usuario.
            var urlRetornoDocs = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno", urlRetornoDocs);

            //Obtenemos el ID de la persona.
            var PersonaDoc = this._context.PersonasDb
                .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonaDoc.per_codigo_id;

            var oDocumentoDisponible = this._context.IdentidadPersonasDb
            .FirstOrDefault(x => x.ide_codigo_id == id);
            ViewBag.IdDocumentoDisponible = oDocumentoDisponible;

            //Obtenemos el ID del documento asociado a la persona.
            var oDocumento = this._context.IdentidadPersonasDb
                .Count(x => x.ide_codigo_id == id);
            ViewBag.IdDocumento = oDocumento;

            var NombrePersonaDocumento = this._context.PersonasDb
               .Where(x => x.per_codigo_id == id)
               .Select(x => x.per_primer_nom).FirstOrDefault();
            ViewBag.NombrePersonaDocumento = NombrePersonaDocumento;

           // var segNombrePersonaDocumento = this._context.PersonasDb
           //.Where(x => x.per_codigo_id == id)
           //.Select(x => x.per_segundo_nom).FirstOrDefault();
           // ViewBag.segNombrePersonaDocumento = segNombrePersonaDocumento;

            var primerApeDocumento = this._context.PersonasDb
           .Where(x => x.per_codigo_id == id)
           .Select(x => x.per_primer_ape).FirstOrDefault();
            ViewBag.primerApeDocumento = primerApeDocumento;

            if (ViewBag.IdDocumento == 0)
            {
                TempData["msjDocsDisponibles"] = "La persona no tiene documentos agregados...";
            }

            if (id == null)
            {
                return NotFound();
            }

            DocumentosDisponiblesPersona(PersonaDoc.per_codigo_id);

            return View();
        }


        #endregion Area Documentos


        #region Método crea lista de documentos disponibles de la persona
        public void DocumentosDisponiblesPersona(int idPersona)

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
                                  tid_descripcion= TipoDoc.tid_descripcion,
                                  per_segundo_nom = persona.per_segundo_nom,
                                  ide_codigo_id = identidad.ide_codigo_id,    
                                 
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

    }
}
