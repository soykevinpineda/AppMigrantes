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
using Migrantes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migrantes.Controllers
{
    public class FiadorController : GenericController
    {
        private readonly ApplicationDbContext _context;
        private readonly IDocumentos _documentos;
        private readonly IPersonas _persona;
        private readonly IFamiliares _familiares;
        private readonly IFiador _fiador;

        //Método constructor: aqui se van agregando las Interfaces creadas.
        public FiadorController(ApplicationDbContext context,
            IDocumentos documentos, IPersonas persona, IFamiliares agregandofamiliares, IFiador fiador)
        {
            this._fiador = fiador;
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


        #region Método Exportar Fiador a Excel

        public static List<FiadorViewModel> oFiadorExcel;

        public FileResult ExportarFiadorExcel(string[] nombrePropiedades)
        {
            byte[] buffer = ExportarExcelGeneric(nombrePropiedades, oFiadorExcel);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        #endregion Método Exportar Documentos a Excel


        #region Área fiador


        //Get: Crear fiador asociado al ID de la persona selecionada.
        [HttpGet]
        public async Task<IActionResult> CrearFiador(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            await ObtenerPersona(id);
            Sexos();
            NombrePersonaSeleccionada(id);

            return await Task.Run(() => View());
        }


        //Post: Guardando al fiador creado.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuardarFiador(FiadorViewModel FiadorCreado)
        {

            Sexos();

            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                else
                {
                    await this._fiador.GuardarFiador(FiadorCreado);
                    TempData["msjGuardadoExitoso"] = "El fiador fue creado exitosamente!";
                }
            }

            catch (Exception)
            {
                throw;
            }

            var urlRetornoFiadorDisponible = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoFiadorDisponible);
        }


        //Post: Editar fiador.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarFiadorEditado(FiadorViewModel oFiadorEditado)
        {

            if (oFiadorEditado == null)
            {
                return NotFound();
            }

            await ObtenerPersona(oFiadorEditado.per_codigo_id);
            SexosEditar();

            try
            {
                await this._fiador.GuardarFiadorEditado(oFiadorEditado);
                TempData["alertaFiadorEditadoOK"] = "¡Fiador actualizado exitosamente!";
            }
            catch (Exception)
            {
                throw;
            }

            var urlRetornoFiadorDetalles = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoFiadorDetalles);
        }


        public async Task<IActionResult> FiadorDisponible(int? id)
        {
            //Obtenemos la ruta de inicio del usuario.
            var urlRetornoFiadorDisponible = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno", urlRetornoFiadorDisponible);

            if (id == null)
            {
                return NotFound();
            }

            await ObtenerPersona(id);
            NombrePersonaSeleccionada(id);

            //Obtenemos el ID del Fiador si esta disponible en la DB.
            var FiadorObtenido = this._context.FiadorDb.AsNoTracking()
                .FirstOrDefault(x => x.per_codigo_id == id);

            ViewBag.FiadorID = FiadorObtenido;

            //Se llenan alertas si el fiador no es encontrado en la DB.
            if (ViewBag.FiadorID == null)
            {
                TempData["msjSinFiador1"] = "La persona seleccionada";
                TempData["msjSinFiador2"] = "no posee un fiador, desea agregarlo?";
            }

            //Obtenemos el fiador si es encontrado en la DB.
            if (ViewBag.FiadorID != null)
            {
                Fiador(FiadorObtenido.per_codigo_id);
            }

            List<FiadorViewModel> Fiador_exportarExcel = new List<FiadorViewModel>();

            Fiador_exportarExcel = (from p in this._context.PersonasDb
                                    join f in this._context.FiadorDb
                                    on p.per_codigo_id equals f.per_codigo_id
                                    join s in this._context.SexosDb
                                    on p.per_codigo_id equals s.id_sexo
                                    where p.per_codigo_id == id

                                    select new FiadorViewModel
                                    {
                                        per_codigo_id = f.per_codigo_id,
                                        FiadorID = f.FiadorID,
                                        PrimerNombreDelFiador = f.PrimerNombreDelFiador,
                                        SegundoNombreDelFiador = f.SegundoNombreDelFiador,
                                        ApellidosDelFiador = f.ApellidosDelFiador,
                                        FechaNacimientoDelFiador = f.FechaNacimientoDelFiador,
                                        EdadDelFiador = f.EdadDelFiador,
                                        SexoDelFiador = s.nomenclatura_sexo,
                                        PaisNacimientoDelFiador = f.PaisNacimientoDelFiador,
                                        EmailFiador = f.EmailFiador,
                                        TelefonoFiador = f.TelefonoFiador,
                                        TelefonoAlternoFiador = f.TelefonoAlternoFiador,
                                        NumCartasPersonales = f.NumCartasPersonales,
                                        NumCartasFamiliares = f.NumCartasFamiliares,
                                        EntregoRecibo_Agua_o_Luz = f.EntregoRecibo_Agua_o_Luz,
                                        FechaGrabacionDelFiador = f.FechaGrabacionDelFiador

                                    }).ToList();

            oFiadorExcel = Fiador_exportarExcel;

            return await Task.Run(() => View(Fiador_exportarExcel));

        }

        //Get: Editar fiador.
        [HttpGet]
        public async Task<IActionResult> EditarFiador(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            await ObtenerPersona(id);

            var Fiador_Persona = await this._context.FiadorDb
               .FirstOrDefaultAsync(x => x.per_codigo_id == id);

            if (Fiador_Persona == null)
            {
                return NotFound();
            }

            Sexos();
            NombrePersonaSeleccionada(id);

            var objFiadorEditado = new FiadorViewModel()

            {
                per_codigo_id = Fiador_Persona.per_codigo_id,
                FiadorID = Fiador_Persona.FiadorID,
                PrimerNombreDelFiador = Fiador_Persona.PrimerNombreDelFiador,
                SegundoNombreDelFiador = Fiador_Persona.SegundoNombreDelFiador,
                ApellidosDelFiador = Fiador_Persona.ApellidosDelFiador,
                FechaNacimientoDelFiador = Fiador_Persona.FechaNacimientoDelFiador,
                EdadDelFiador = Fiador_Persona.EdadDelFiador,
                SexoDelFiador = Fiador_Persona.SexoDelFiador,
                PaisNacimientoDelFiador = Fiador_Persona.PaisNacimientoDelFiador,
                EmailFiador = Fiador_Persona.EmailFiador,
                TelefonoFiador = Fiador_Persona.TelefonoFiador,
                TelefonoAlternoFiador = Fiador_Persona.TelefonoAlternoFiador,
                NumCartasPersonales = Fiador_Persona.NumCartasPersonales,
                NumCartasFamiliares = Fiador_Persona.NumCartasFamiliares,
                EntregoRecibo_Agua_o_Luz = Fiador_Persona.EntregoRecibo_Agua_o_Luz,
                FechaGrabacionDelFiador = Fiador_Persona.FechaGrabacionDelFiador,

            };

            return await Task.Run(() => View(objFiadorEditado));

        }

        //Get: Eliminar fiador.
        [HttpGet]
        public async Task<IActionResult> EliminarFiador(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            await ObtenerPersona(id);
            NombrePersonaSeleccionada(id);
            Sexos();

            var FiadorDeLaPersona = await this._context.FiadorDb.AsNoTracking()
              .FirstOrDefaultAsync(x => x.per_codigo_id == id);

            if (FiadorDeLaPersona == null)
            {
                return NotFound();
            }

            var objEliminarFiador = new FiadorViewModel()
            {
                per_codigo_id = FiadorDeLaPersona.per_codigo_id,
                FiadorID = FiadorDeLaPersona.FiadorID,
                PrimerNombreDelFiador = FiadorDeLaPersona.PrimerNombreDelFiador,
                SegundoNombreDelFiador = FiadorDeLaPersona.SegundoNombreDelFiador,
                ApellidosDelFiador = FiadorDeLaPersona.ApellidosDelFiador,
                FechaNacimientoDelFiador = FiadorDeLaPersona.FechaNacimientoDelFiador,
                EdadDelFiador = FiadorDeLaPersona.EdadDelFiador,
                SexoDelFiador = FiadorDeLaPersona.SexoDelFiador,
                PaisNacimientoDelFiador = FiadorDeLaPersona.PaisNacimientoDelFiador,
                EmailFiador = FiadorDeLaPersona.EmailFiador,
                TelefonoFiador = FiadorDeLaPersona.TelefonoFiador,
                TelefonoAlternoFiador = FiadorDeLaPersona.TelefonoAlternoFiador,
                NumCartasPersonales = FiadorDeLaPersona.NumCartasPersonales,
                NumCartasFamiliares = FiadorDeLaPersona.NumCartasFamiliares,
                EntregoRecibo_Agua_o_Luz = FiadorDeLaPersona.EntregoRecibo_Agua_o_Luz,
                FechaGrabacionDelFiador = FiadorDeLaPersona.FechaGrabacionDelFiador,

            };

            return View(objEliminarFiador);
        }


        //Post: Eliminar fiador.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarFiadorConfirmado(FiadorViewModel oFiadorEliminado)
        {

            await ObtenerPersona(oFiadorEliminado.per_codigo_id);

            var FiadorDePersona = await this._context.FiadorDb.AsNoTracking()
               .AsNoTracking().FirstOrDefaultAsync(x => x.FiadorID == oFiadorEliminado.FiadorID);

            if (oFiadorEliminado == null || FiadorDePersona == null)
            {
                return NotFound();
            }

            try
            {
                await this._fiador.EliminarFiadorConfirmado(oFiadorEliminado);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Personas", "Personas");
        }


        #endregion Region fiador.


        #region Método crea una lista del fiador disponible de la persona.
        public void Fiador(int PersonaID)
        {
            List<FiadorViewModel> ListaFiador = new List<FiadorViewModel>();

            ListaFiador = (from persona in this._context.PersonasDb
                           join fiador in this._context.FiadorDb
                           on persona.per_codigo_id equals fiador.per_codigo_id
                           join s in this._context.SexosDb
                           on fiador.per_codigo_id equals s.id_sexo
                           where persona.per_codigo_id == PersonaID

                           select new FiadorViewModel
                           {
                               per_codigo_id = persona.per_codigo_id,
                               FiadorID = fiador.FiadorID,
                               PrimerNombreDelFiador = fiador.PrimerNombreDelFiador,
                               SegundoNombreDelFiador = fiador.SegundoNombreDelFiador,
                               ApellidosDelFiador = fiador.ApellidosDelFiador,
                               FechaNacimientoDelFiador = fiador.FechaNacimientoDelFiador,
                               EdadDelFiador = fiador.EdadDelFiador,
                               SexoDelFiador = s.nomenclatura_sexo,
                               PaisNacimientoDelFiador = fiador.PaisNacimientoDelFiador,
                               EmailFiador = fiador.EmailFiador,
                               TelefonoFiador = fiador.TelefonoFiador,
                               TelefonoAlternoFiador = fiador.TelefonoAlternoFiador,
                               NumCartasPersonales = fiador.NumCartasPersonales,
                               NumCartasFamiliares = fiador.NumCartasFamiliares,
                               EntregoRecibo_Agua_o_Luz = fiador.EntregoRecibo_Agua_o_Luz,
                               FechaGrabacionDelFiador = fiador.FechaGrabacionDelFiador,

                           }).ToList();

            ViewBag.FiadorObtenido = ListaFiador;

        }
        #endregion


        #region Procedimientos Sexo 
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
        #endregion Procedimientos Sexo 



        #region Mètodo para seleccionar nombres de la persona.
        //Crea una lista con nombres de la persona
        public void NombrePersonaSeleccionada(int? PersonaID)

        {
            List<PersonaDTO> ListNombres = new List<PersonaDTO>();

            ListNombres = (from p in this._context.PersonasDb
                           where p.per_codigo_id == PersonaID

                           select new PersonaDTO

                           {
                               per_codigo_id = p.per_codigo_id,
                               per_primer_nom = p.per_primer_nom,
                               per_segundo_nom = p.per_segundo_nom,
                               per_primer_ape = p.per_primer_ape,
                               per_segundo_ape = p.per_segundo_ape,

                           }).ToList();

            ViewBag.NombresPersona = ListNombres;

        }
        #endregion Mètodo para seleccionar nombres de la persona.

    }

}


