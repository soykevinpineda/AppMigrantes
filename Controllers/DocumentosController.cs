using AutoMapper;
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


        public async Task ObtenerPersona(int? id)
        {

            var GetPerson = await this._context.PersonasDb
                .FirstOrDefaultAsync(x => x.per_codigo_id == id);

            ViewBag.PersonaID = GetPerson.per_codigo_id;
        }



        #region Area documentos de la persona


        //GET: Creamos documento de la persona
        [HttpGet]
        public async Task<IActionResult> CrearDocumento(int? id)
        {
        
            await ObtenerPersona(id);
            PersonaSeleccionada(id);
            TipoDoc(id);

            return await Task.Run(() => View());
        }


        //POST: Guardamos el documento asociado al id de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuardarDoc(DocumentosPersonaDTO identidad)
        {
            var files = HttpContext.Request.Form.Files;

            if (ModelState.IsValid)
            {
                try
                {
                    //Guardando Imagenes de Documentos: Lado A.

                    string wwwRothPath = this._IWebHostEnvironment.WebRootPath;
                    string fileNameA = Path.GetFileNameWithoutExtension(identidad.FotoDocumento.FileName);
                    string extension = Path.GetExtension(identidad.FotoDocumento.FileName);
                    identidad.NombreImagenPortada_A = fileNameA = fileNameA + DateTime.Now.ToString("_ddMMyyyy-HHmmssffff") + extension;
                    string path = Path.Combine(wwwRothPath + "/DocumentosAdjuntos/DocsLadoA/", fileNameA);
                    string path2 = Path.Combine(RutaImagen1 + fileNameA);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        identidad.FotoDocumento.CopyTo(fileStream);
                    }


                    //Guardando Imagenes de Documentos: Lado B.

                    string wwwRothPathB = this._IWebHostEnvironment.WebRootPath;
                    string fileNameB = Path.GetFileNameWithoutExtension(identidad.FotoDocumento_LadoB.FileName);
                    string extensionB = Path.GetExtension(identidad.FotoDocumento_LadoB.FileName);
                    identidad.NombreImagenPortada_B = fileNameB = fileNameB + DateTime.Now.ToString("_ddMMyyyy-HHmmssffff") + extension;
                    string pathB = Path.Combine(wwwRothPathB + "/DocumentosAdjuntos/DocsLadoB/", fileNameB);
                    string path2B = Path.Combine(RutaImagen2 + fileNameB);

                    using (var fileStream = new FileStream(pathB, FileMode.Create))
                    {

                        identidad.FotoDocumento_LadoB.CopyTo(fileStream);
                    }

                    TempData["alertaDocGuardadoOK"] = "¡Documento guardado exitosamente!";
                    await this._documentos.GuardarDocumento(identidad, fileNameA, path2, fileNameB, path2B);
                }

                catch (Exception)
                {
                    throw;
                }

            }

            var urlRetornoDocsDisponibles = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDocsDisponibles);
        }


        //GET: Editar documento de la persona
        [HttpGet]
        public async Task<IActionResult> EditarDocumento(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await this._context.IdentidadPersonasDb
                .FirstOrDefaultAsync(x => x.ide_id_persona == id);

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
                FotoDocumento = documento.FotoDocumento,
                FotoDocumento_LadoB = documento.FotoDocumento_LadoB,
                NombreImagenPortada_A = documento.NombreImagenPortada_A,
                RutaImagenPortada_A = documento.RutaImagenPortada_A,
                NombreImagenPortada_B = documento.NombreImagenPortada_B,
                RutaImagenPortada_B = documento.RutaImagenPortada_B,

            };

            PersonaSeleccionada(Doc.ide_codigo_id);
            TipoDoc(Doc.ide_id_persona);

            return View(Doc);
        }


        //POST: Guardar documento ya editado de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarDocumento(IdentidadPersona DocumentoEditado)
        {

            var objDocumento = await this._context.IdentidadPersonasDb.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ide_id_persona == DocumentoEditado.ide_id_persona);

            var files = HttpContext.Request.Form.Files;


            try
            {
                //Si se carga una nueva Imagen por el usuario.
                if (files.Count > 0)
                {
                    //Guardando Imagen del Documento: Lado A.
                    string wwwRothPath = this._IWebHostEnvironment.WebRootPath;
                    string fileNameA = Path.GetFileNameWithoutExtension(DocumentoEditado.FotoDocumento.FileName);
                    string extension = Path.GetExtension(DocumentoEditado.FotoDocumento.FileName);
                    DocumentoEditado.NombreImagenPortada_A = fileNameA = fileNameA + DateTime.Now.ToString("_ddMMyyyy-HHmmssffff") + extension;
                    string path2 = Path.Combine(RutaImagen1 + fileNameA);
                    string path = Path.Combine(wwwRothPath + "/DocumentosAdjuntos/DocsLadoA/", fileNameA);
                    string path_anteriorA = Path.Combine(wwwRothPath + "/DocumentosAdjuntos/DocsLadoA/");


                    //Obtenemos la ruta y el objeto para borrar LADO A.
                    var ImagenAnteriorA = Path.Combine(path_anteriorA + objDocumento.NombreImagenPortada_A);

                    //Inicio de borrado de imagen anterior LADO A.
                    if (System.IO.File.Exists(ImagenAnteriorA))
                    {
                        System.IO.File.Delete(ImagenAnteriorA);
                    }
                    //Fin borrar imagen anterior.


                    //Se crea la nueva imagen LADO A.
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        DocumentoEditado.FotoDocumento.CopyTo(fileStream);
                    }


                    //Guardando Imagen del Documento: Lado B.
                    string wwwRothPathB = this._IWebHostEnvironment.WebRootPath;
                    string fileNameB = Path.GetFileNameWithoutExtension(DocumentoEditado.FotoDocumento_LadoB.FileName);
                    string extensionB = Path.GetExtension(DocumentoEditado.FotoDocumento_LadoB.FileName);
                    DocumentoEditado.NombreImagenPortada_B = fileNameB = fileNameB + DateTime.Now.ToString("_ddMMyyyy-HHmmssffff") + extension;
                    string pathB = Path.Combine(wwwRothPathB + "/DocumentosAdjuntos/DocsLadoB/", fileNameB);
                    string path2B = Path.Combine(RutaImagen2 + fileNameB);
                    string path_anteriorB = Path.Combine(wwwRothPath + "/DocumentosAdjuntos/DocsLadoB/");


                    //Obtenemos la ruta y el objeto para borrar LADO B.
                    var ImagenAnteriorB = Path.Combine(path_anteriorB + objDocumento.NombreImagenPortada_B);

                    //Inicio de borrado de imagen anterior LADO B.
                    if (System.IO.File.Exists(ImagenAnteriorB))
                    {
                        System.IO.File.Delete(ImagenAnteriorB);
                    }
                    //Fin borrar imagen anterior.


                    //Se crea la nueva imagen LADO B.
                    using (var fileStream = new FileStream(pathB, FileMode.Create))
                    {

                        DocumentoEditado.FotoDocumento_LadoB.CopyTo(fileStream);
                    }

                    DocumentoEditado.RutaImagenPortada_A = RutaImagen1 + fileNameA;
                    DocumentoEditado.RutaImagenPortada_B = RutaImagen2 + fileNameB;
                }

                //Si no cargo nuevas fotos se asignan las fotografias antiguas.
                else
                {

                    DocumentoEditado.RutaImagenPortada_A = objDocumento.RutaImagenPortada_A;
                    DocumentoEditado.RutaImagenPortada_B = objDocumento.RutaImagenPortada_B;

                }

                await this._documentos.GuardarDocumentoEditado(DocumentoEditado);
                TempData["alertaDocsEditadoOK"] = "¡Documento editado exitosamente!";

            }
            catch (Exception)
            {

                throw;
            }

            var urlRetornoDocsDisponibles = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDocsDisponibles);
        }


        //GET: Detalles del documento de la persona
        [HttpGet]
        public async Task<IActionResult> DetallesDocumento(int? id)
        {
            var documento = await this._context.IdentidadPersonasDb.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ide_id_persona == id);

            PersonaSeleccionada(documento.ide_codigo_id);
            DocumentosDisponiblesPersona(documento.ide_codigo_id);
            TipoDoc(documento.ide_id_persona);

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
                ide_fecha_vencimiento = documento.ide_fecha_vencimiento,
                RutaImagenPortada_A = documento.RutaImagenPortada_A,
                RutaImagenPortada_B = documento.RutaImagenPortada_B
            };

            return View(doc);
        }


        //GET: Eliminar documento de la persona
        [HttpGet]
        public async Task<IActionResult> EliminarDocumento(int? id)
        {

            var documento = await this._context.IdentidadPersonasDb.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ide_id_persona == id);

            if (id == null)
            {
                return NotFound();
            }

            if (documento == null)
            {
                var urlRetornoDocsDisponibles = HttpContext.Session.GetString("UrlRetorno");
                return LocalRedirect(urlRetornoDocsDisponibles);
            }

            TipoDoc(documento.ide_id_persona);

            var Doc = new DocumentosViewModel()
            {
                ide_codigo_id = documento.ide_codigo_id,
                ide_id_persona = documento.ide_id_persona,
                ide_id_documento = documento.ide_id_documento,
                ide_numero = documento.ide_numero,
                ide_fecha_emision = documento.ide_fecha_emision,
                ide_fecha_vencimiento = documento.ide_fecha_vencimiento,
                NombreImagenPortada_A = documento.NombreImagenPortada_A,
                RutaImagenPortada_A = documento.RutaImagenPortada_A,
                NombreImagenPortada_B = documento.NombreImagenPortada_B,
                RutaImagenPortada_B = documento.RutaImagenPortada_B,
                ide_estado = 1,
                ide_entregado = true,
            };

            return View(Doc);
        }


        //POST: Eliminar documento.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarDocumentoConfirmado(DocumentosViewModel IdentidadEliminada)
        {

            var documento = await this._context.IdentidadPersonasDb.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ide_id_persona == IdentidadEliminada.ide_id_persona);

            TipoDoc(documento.ide_id_persona);

            if (IdentidadEliminada == null)
            {
                return NotFound();

            }

            try
            {
                //Obtenemos la imagen Lado A.
                string wwwRothPath = this._IWebHostEnvironment.WebRootPath;
                string path_anteriorA = Path.Combine(wwwRothPath + "/DocumentosAdjuntos/DocsLadoA/");

                //Obtenemos la ruta y el objeto para borrar LADO A.
                var ImagenAnteriorA = Path.Combine(path_anteriorA + documento.NombreImagenPortada_A);

                //Inicio de borrado de imagen anterior LADO A.
                if (System.IO.File.Exists(ImagenAnteriorA))
                {
                    System.IO.File.Delete(ImagenAnteriorA);
                }
                //Fin borrar imagen anterior.


                //Obtenemos la imagen Lado B.
                string wwwRothPathB = this._IWebHostEnvironment.WebRootPath;
                string path_anteriorB = Path.Combine(wwwRothPath + "/DocumentosAdjuntos/DocsLadoB/");

                //Obtenemos la ruta y el objeto para borrar LADO B.
                var ImagenAnteriorB = Path.Combine(path_anteriorB + documento.NombreImagenPortada_B);

                //Inicio de borrado de imagen anterior LADO B.
                if (System.IO.File.Exists(ImagenAnteriorB))
                {
                    System.IO.File.Delete(ImagenAnteriorB);
                }
                //Fin borrar imagen anterior.

                await this._documentos.EliminarDocumentoConfirmado(IdentidadEliminada);
            }
            catch (Exception)
            {
                throw;
            }

            var urlRetornoDocsDisponibles = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDocsDisponibles);
        }


        [HttpGet]
        public IActionResult DocumentosDisponibles(int? id)
        {
            //Obtenemos la ruta de inicio del usuario.
            var urlRetornoDocsDisponibles = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno", urlRetornoDocsDisponibles);

            if (id == null)
            {
                return NotFound();
            }

            //Obtenemos el ID de la persona asociada al documento.
            var PersonaDoc = this._context.PersonasDb.AsNoTracking()
                    .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonaDoc.per_codigo_id;

            if (ViewBag.IdPersona == null)
            {
                return NotFound();
            }

            //Llamamos métodos que crean lista de documentos,selecciona el tipo documento
            //y selecciona el nombre de la persona recibiendo el ID como parámetro.
            DocumentosDisponiblesPersona(PersonaDoc.per_codigo_id);
            TipoDoc(PersonaDoc.per_codigo_id);
            PersonaSeleccionada(PersonaDoc.per_codigo_id);

            //Obtenemos el ID del documento asociado a la persona.
            var oDocumento = this._context.IdentidadPersonasDb.AsNoTracking()
                .Count(x => x.ide_codigo_id == id);
            ViewBag.IdDocumentoDisponible = oDocumento;

            if (ViewBag.IdDocumentoDisponible == 0)
            {
                TempData["alertaDocs1"] = "La persona seleccionada";
                TempData["alertaDocs2"] = "no posee documentos, desea agregarlos?";
            }

            List<DocumentosPersonaDTO> Documentos_exportarExcel = new List<DocumentosPersonaDTO>();

            Documentos_exportarExcel = (from identidad in this._context.IdentidadPersonasDb
                                        join persona in this._context.PersonasDb
                                        on identidad.ide_codigo_id equals persona.per_codigo_id
                                        join TipoDoc in this._context.TipoDocumentosDb
                                        on identidad.ide_id_documento equals TipoDoc.tid_id_documento
                                        where persona.per_codigo_id == id


                                        select new DocumentosPersonaDTO

                                        {
                                            tid_descripcion = TipoDoc.tid_descripcion,
                                            ide_codigo_id = identidad.ide_codigo_id,
                                            RutaImagenPortada_A = identidad.RutaImagenPortada_A,
                                            NombreImagenPortada_A = identidad.NombreImagenPortada_A,
                                            RutaImagenPortada_B = identidad.RutaImagenPortada_B,
                                            NombreImagenPortada_B = identidad.NombreImagenPortada_B,
                                            ide_id_persona = identidad.ide_id_persona,
                                            NombreDocumento = TipoDoc.tid_descripcion,
                                            per_codigo_id = persona.per_codigo_id,
                                            per_primer_nom = persona.per_primer_nom,
                                            per_segundo_nom = persona.per_segundo_nom,
                                            per_primer_ape = persona.per_primer_ape,
                                            ide_id_documento = identidad.ide_id_documento,
                                            ide_numero = identidad.ide_numero,
                                            ide_fecha_emision = identidad.ide_fecha_emision,
                                            ide_fecha_vencimiento = identidad.ide_fecha_vencimiento

                                        }).ToList();

            oDocumentosExcel = Documentos_exportarExcel;

            return View(Documentos_exportarExcel);
        }


        #endregion Area Documentos


        #region Método Exportar Documentos a Excel

        public static List<DocumentosPersonaDTO> oDocumentosExcel;


        public FileResult ExportarDocsExcel(string[] nombrePropiedades)
        {
            byte[] buffer = ExportarExcelGeneric(nombrePropiedades, oDocumentosExcel);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        #endregion Método Exportar Documentos a Excel


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
                                  tid_descripcion = TipoDoc.tid_descripcion,
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

            oDocumentosExcel = ListDocumentos;

            if (ViewBag.ListadoDocumentos.Count == 3)
            {
                TempData["msjDocsTotales"] = "¡La persona seleccionada ya posee todos sus documentos!";
            }
        }
        #endregion


        #region Método lista tipo de documentos de la persona

        //Método crea una lista den Tipos de documentos guardados en la DB.
        public void TipoDoc(int? id)
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

        #endregion Métodos lista de tipo de documentos de la persona


        #region Mètodo crea lista con los nombres de la persona seleccionada.
        //Crea una lista con nombres de la persona
        public void PersonaSeleccionada(int? IdPersona)

        {
            List<DocumentosPersonaDTO> ListNombres = new List<DocumentosPersonaDTO>();

            ListNombres = (from p in this._context.PersonasDb
                           where p.per_codigo_id == IdPersona

                           select new DocumentosPersonaDTO

                           {
                               per_codigo_id = p.per_codigo_id,
                               per_primer_nom = p.per_primer_nom,
                               per_segundo_nom = p.per_segundo_nom,
                               per_primer_ape = p.per_primer_ape,
                               per_segundo_ape = p.per_segundo_ape,

                           }).ToList();

            ViewBag.NombresPersona = ListNombres;

        }
        #endregion Mètodo crea lista con los nombres de la persona seleccionada.



    }
}
