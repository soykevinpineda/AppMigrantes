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


        #region Área fiador.

        //Get: Crear fiador asociado al ID de la persona selecionada.
        [HttpGet]
        public IActionResult CrearFiador(int? id)
        {

            var urlRetornoFiadorCrear = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno", urlRetornoFiadorCrear);


            if (id == null)
            {
                return NotFound();
            }

            var Fiador_Persona = this._context.FiadorDb
           .FirstOrDefault(x => x.per_codigo_id == id);

            Sexos();


            if (Fiador_Persona == null)
            {

                TempData["alertaFiador"] = "La persona seleccionada no tiene un fiador, desea agregarlo?";
            }

            ViewBag.FiadorPersona = Fiador_Persona;

            var Codigo_Persona = this._context.PersonasDb
            .FirstOrDefault(x => x.per_codigo_id == id);

            ViewBag.IdPersona = Codigo_Persona.per_codigo_id;

            ListFiador(Codigo_Persona.per_codigo_id);

            var NombrePersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_primer_nom).FirstOrDefault();
            ViewBag.Nombre_Persona = NombrePersona;

            var SegNombrePersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
             .Select(x => x.per_segundo_nom).FirstOrDefault();
            ViewBag.SegNombrePersona = SegNombrePersona;


            var ApellidoPersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_primer_ape).FirstOrDefault();
            ViewBag.Apellido_Persona = ApellidoPersona;

            return View();
        }


        //Post: Guardando al fiador creado.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuardarFiador(FiadorViewModel FiadorCreado)
        {

            var urlRetornoFiadorCrear = HttpContext.Session.GetString("UrlRetorno");

            try
            {
                if (!ModelState.IsValid)
                {

                    TempData["msjSinGuardar"] = "No se agrego al fiador, recuerda llenar todos los campos solicitados...";
                    return LocalRedirect(urlRetornoFiadorCrear);

                }
                else
                {
                    TempData["msjGuardadoExitoso"] = "El fiador fue creado exitosamente!";
                    await this._fiador.GuardarFiador(FiadorCreado);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Personas", "Personas");

        }


        //Get: Muestra detalles del fiador asociado al ID de la persona.
        [HttpGet]
        public IActionResult DetallesFiador(int? id)

        {

            //Obtenemos la ruta de inicio del usuario.
            var urlRetornoFiadorDetalles = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno", urlRetornoFiadorDetalles);

            if (id == null)
            {
                return NotFound();
            }

            var ID_Persona = this._context.PersonasDb
              .FirstOrDefault(x => x.per_codigo_id == id);

            ViewBag.IdPersona = ID_Persona.per_codigo_id;

            var Fiador_Persona = this._context.FiadorDb
                .FirstOrDefault(x => x.per_codigo_id == id);

            if (Fiador_Persona == null)
            {
                TempData["alertaFiador"] = "La persona seleccionada no tiene un fiador, desea agregarlo?";
            }

            ViewBag.FiadorPersona = Fiador_Persona;

            var PrimerNombrePersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_primer_nom).FirstOrDefault();
            ViewBag.Nombre_Persona = PrimerNombrePersona;

            var SegNombrePersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_segundo_nom).FirstOrDefault();
            ViewBag.SegNombrePersona = SegNombrePersona;

            var ApellidoPersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_primer_ape).FirstOrDefault();
            ViewBag.Apellido_Persona = ApellidoPersona;

            ListFiador(ID_Persona.per_codigo_id);

            return View();

        }



        //Get: Eliminar fiador.
        [HttpGet]
        public async Task<IActionResult> EliminarFiador(int? id)
        {
            Sexos();

            if (id == null)
            {
                return NotFound();
            }

            var Fiador_Persona = await this._context.FiadorDb
                .FirstOrDefaultAsync(x => x.per_codigo_id == id);

            if (Fiador_Persona == null)
            {
                return NotFound();
            }

            var ID_Persona = this._context.PersonasDb
           .FirstOrDefault(x => x.per_codigo_id == id);

            ViewBag.IdPersona = ID_Persona.per_codigo_id;

            var NombrePersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_primer_nom).FirstOrDefault();
            ViewBag.Nombre_Persona = NombrePersona;

            var SegNombrePersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_segundo_nom).FirstOrDefault();
            ViewBag.SegNombrePersona = SegNombrePersona;

            var ApellidoPersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_primer_ape).FirstOrDefault();
            ViewBag.Apellido_Persona = ApellidoPersona;


            var objEliminarFiador = new FiadorViewModel()

            {

                per_codigo_id = Fiador_Persona.per_codigo_id,
                IdFiador = Fiador_Persona.IdFiador,
                PrimerNombreFiador = Fiador_Persona.PrimerNombreFiador,
                SegundoNombreFiador = Fiador_Persona.SegundoNombreFiador,
                PrimerApellidoFiador = Fiador_Persona.PrimerApellidoFiador,
                SegundoApellidoFiador = Fiador_Persona.SegundoApellidoFiador,
                PaisNacimientoFiador = Fiador_Persona.PaisNacimientoFiador,
                EdadFiador = Fiador_Persona.EdadFiador,
                SexoFiador = Fiador_Persona.SexoFiador,
                EmailFiador = Fiador_Persona.EmailFiador,
                TelefonoFiador = Fiador_Persona.TelefonoFiador,
                TelefonoAlternoFiador = Fiador_Persona.TelefonoAlternoFiador,
                EntregoRecibo_Agua_o_Luz = Fiador_Persona.EntregoRecibo_Agua_o_Luz,
                NumCartasPersonales = Fiador_Persona.NumCartasPersonales,
                NumCartasFamiliares = Fiador_Persona.NumCartasFamiliares

            };

            return View(objEliminarFiador);
        }


        //Post: Eliminar fiador.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarFiadorConfirmado(FiadorViewModel oFiadorEliminado)
        {

            var ID_Persona = this._context.PersonasDb
            .FirstOrDefault(x => x.per_codigo_id == oFiadorEliminado.per_codigo_id);

            ViewBag.IdPersona = ID_Persona.per_codigo_id;

            var FiadorDePersona = await this._context.FiadorDb
               .AsNoTracking().FirstOrDefaultAsync(x => x.IdFiador == oFiadorEliminado.IdFiador);


            if (FiadorDePersona == null)
            {
                return NotFound();
            }

            if (oFiadorEliminado == null)
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


        //Get: Editar fiador.
        [HttpGet]
        public async Task<IActionResult> EditarFiador(int? id)
        {

            var ID_Persona = this._context.PersonasDb
            .FirstOrDefault(x => x.per_codigo_id == id);

            ViewBag.IdPersona = ID_Persona.per_codigo_id;

            if (id == null)
            {
                return NotFound();
            }

            Sexos();
            SexosEditar();

            if (ID_Persona == null)
            {
                return NotFound();
            }

            var Fiador_Persona = await this._context.FiadorDb
                .AsNoTracking().FirstOrDefaultAsync(x => x.per_codigo_id == id);

            if (Fiador_Persona == null)
            {
                return NotFound();
            }

            var objFiadorEditado = new FiadorViewModel()

            {

                per_codigo_id = Fiador_Persona.per_codigo_id,
                IdFiador = Fiador_Persona.IdFiador,
                PrimerNombreFiador = Fiador_Persona.PrimerNombreFiador,
                SegundoNombreFiador = Fiador_Persona.SegundoNombreFiador,
                PrimerApellidoFiador = Fiador_Persona.PrimerApellidoFiador,
                SegundoApellidoFiador = Fiador_Persona.SegundoApellidoFiador,
                PaisNacimientoFiador = Fiador_Persona.PaisNacimientoFiador,
                EdadFiador = Fiador_Persona.EdadFiador,
                SexoFiador = Fiador_Persona.SexoFiador,
                EmailFiador = Fiador_Persona.EmailFiador,
                TelefonoFiador = Fiador_Persona.TelefonoFiador,
                TelefonoAlternoFiador = Fiador_Persona.TelefonoAlternoFiador,
                EntregoRecibo_Agua_o_Luz = Fiador_Persona.EntregoRecibo_Agua_o_Luz,
                NumCartasPersonales = Fiador_Persona.NumCartasPersonales,
                NumCartasFamiliares = Fiador_Persona.NumCartasFamiliares

            };

            return View(objFiadorEditado);
        }


        //Post: Editar fiador.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarFiadorEditado(FiadorViewModel oFiadorEditado)
        {

            var ID_Persona = this._context.PersonasDb.AsNoTracking()
             .FirstOrDefault(x => x.per_codigo_id == oFiadorEditado.per_codigo_id);

            if (ID_Persona == null)
            {
                return NotFound();
            }

            if (oFiadorEditado == null)
            {
                return NotFound();
            }

            try
            {
                await this._fiador.GuardarFiadorEditado(oFiadorEditado);
            }
            catch (Exception)
            {
                throw;
            }

            var urlRetornoFiadorDetalles = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoFiadorDetalles);
        }



        #endregion Region fiador.


        #region Método crea una lista del fiador disponible de la persona.
        public void ListFiador(int IdPersona)

        {
            List<FiadorViewModel> ListFiador = new List<FiadorViewModel>();

            ListFiador = (from fiador in this._context.FiadorDb
                          join persona in this._context.PersonasDb
                           on fiador.per_codigo_id equals persona.per_codigo_id
                          where persona.per_codigo_id == IdPersona


                          select new FiadorViewModel

                          {
                              per_codigo_id = persona.per_codigo_id,
                              IdFiador = fiador.IdFiador,
                              PrimerNombreFiador = fiador.PrimerNombreFiador,
                              SegundoNombreFiador = fiador.SegundoNombreFiador,
                              PrimerApellidoFiador = fiador.PrimerApellidoFiador,
                              SegundoApellidoFiador = fiador.SegundoApellidoFiador,
                              EdadFiador = fiador.EdadFiador,
                              SexoFiador = fiador.SexoFiador,
                              PaisNacimientoFiador = fiador.PaisNacimientoFiador,
                              EmailFiador = fiador.EmailFiador,
                              TelefonoFiador = fiador.TelefonoFiador,
                              TelefonoAlternoFiador = fiador.TelefonoAlternoFiador,
                              NumCartasPersonales = fiador.NumCartasPersonales,
                              NumCartasFamiliares = fiador.NumCartasFamiliares,
                              EntregoRecibo_Agua_o_Luz = fiador.EntregoRecibo_Agua_o_Luz,
                              FechaGrabacionFiador = fiador.FechaGrabacionFiador,

                          }).ToList();

            ViewBag.ListadoFiador = ListFiador;

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
    }

}


