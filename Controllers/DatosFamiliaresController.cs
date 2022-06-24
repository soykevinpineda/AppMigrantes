using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migrantes.Data;
using Migrantes.Data.Servicios.Documentos;
using Migrantes.Data.Servicios.Familiares;
using Migrantes.Data.Servicios.Personas;
using Migrantes.Models.DTO;
using Migrantes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migrantes.Controllers
{
    public class DatosFamiliaresController : GenericController
    {

        private readonly ApplicationDbContext _context;
        private readonly IDocumentos _documentos;
        private readonly IPersonas _persona;
        private readonly IFamiliares _familiares;

        //Método constructor: aqui se van agregando las Interfaces creadas.
        public DatosFamiliaresController(ApplicationDbContext context,
            IDocumentos documentos, IPersonas persona, IFamiliares agregandofamiliares)
        {

            this._documentos = documentos;
            this._persona = persona;
            this._familiares = agregandofamiliares;
            this._context = context;
        }



        #region Area Datos de familiares


        //Get: Se agregan datos de familiares asociado al ID de la persona
        [HttpGet]
        public IActionResult CrearDatosFamiliares(int? id)

        {
            //Obtenemos el ID de la persona.
            var PersonDatoFam = this._context.PersonasDb
                .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonDatoFam.per_codigo_id;

            //Obtenemos el ID del dato familiar asociado a la persona.
            var ValidacionDatosFamiliares = this._context.DatosFamiliaresDb
                .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdDatosFam = ValidacionDatosFamiliares;

            if (ValidacionDatosFamiliares == null)

            {
                TempData["msjValidacionFamiliar"] = "La persona seleccionada no tiene datos familiares, desea agregarlos?";
            }

            DatosFamiliaresDisponibles(PersonDatoFam.per_codigo_id);

            PersonaSeleccionada(PersonDatoFam.per_codigo_id);

            return View();

        }


        //Post: Se guardan datos de familiares asociado al ID de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregandoDatosFamiliares(DatosFamiliaresViewModel oDatosFamiliares)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("CrearPersona", "Personas");
                }
                else
                {
                    await this._familiares.AgregandoDatosFamiliares(oDatosFamiliares);
                }
            }

            catch (Exception)
            {
                throw;
            }

            var urlRetornoDatosFamiliares = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDatosFamiliares);
        }


        //Get: Se agregan datos de familiares asociado al ID de la persona
        [HttpGet]
        public IActionResult DetallesDatosFamiliares(int? id)

        {

            //Obtenemos la ruta de inicio del usuario.
            var urlRetornoDatosFamiliares = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno", urlRetornoDatosFamiliares);

            var PersonaDatoFam = this._context.PersonasDb
                .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonaDatoFam.per_codigo_id;

            var ValidacionDatosFamiliares = this._context.DatosFamiliaresDb
                .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdDatosFam = ValidacionDatosFamiliares;

            if (ValidacionDatosFamiliares == null)

            {
                TempData["msjValidacionFamiliar"] = "La persona seleccionada no tiene datos familiares, desea agregarlos?";
            }

            DatosFamiliaresDisponibles(PersonaDatoFam.per_codigo_id);

            return View();

        }

        //Get: Se editan datos de familiares asociado al ID de la persona
        [HttpGet]
        public IActionResult EditarDatosFamiliares(int? id)

        {

            var ObjDatosFamiliares = this._context.DatosFamiliaresDb
                .FirstOrDefault(x => x.per_codigo_id == id);

            if (id == null)
            {
                return NotFound();
            }

            DatosFamiliaresDisponibles(ObjDatosFamiliares.per_codigo_id);

            return View(ObjDatosFamiliares);

        }



        //Post: Se actualiza datos editados de familiares asociado al ID de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarFamiliaresEditados(DatosFamiliaresViewModel DatosFamiliaresEditados)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await this._familiares.ActualizarFamiliaresEditados(DatosFamiliaresEditados);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            var urlRetornoDatosFamiliares = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDatosFamiliares);

        }




        //Get: Se obtiene datos de familiares asociado al ID de la persona
        //para eliminar
        [HttpGet]
        public async Task<IActionResult> EliminarDatosFamiliares(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var eDatosFamiliares = await this._context.DatosFamiliaresDb
                .FirstOrDefaultAsync(x => x.per_codigo_id == id);

            if (eDatosFamiliares == null)
            {
                return NotFound();
            }

            var datosFamiliaresEliminar = new DatosFamiliaresViewModel()

            {
                Id_DatosFamiliares = eDatosFamiliares.Id_DatosFamiliares,
                per_codigo_id = eDatosFamiliares.per_codigo_id,
                PrimerNombreDeLaMadre = eDatosFamiliares.PrimerNombreDeLaMadre,
                SegundoNombreDeLaMadre = eDatosFamiliares.SegundoNombreDeLaMadre,
                ApellidosDeLaMadre = eDatosFamiliares.ApellidosDeLaMadre,            
                FechaNacimientoDeLaMadre= eDatosFamiliares.FechaNacimientoDeLaMadre,
                EdadDeLaMadre = eDatosFamiliares.EdadDeLaMadre,
                PaisNacimientoDeLaMadre = eDatosFamiliares.PaisNacimientoDeLaMadre,
                ProfesionDeLaMadre = eDatosFamiliares.ProfesionDeLaMadre,

                PrimerNombreDelPadre = eDatosFamiliares.PrimerNombreDelPadre,
                SegundoNombreDelPadre = eDatosFamiliares.SegundoNombreDelPadre,
                ApellidosDelPadre = eDatosFamiliares.ApellidosDelPadre,
                FechaNacimientoDelPadre = eDatosFamiliares.FechaNacimientoDelPadre,
                EdadDelPadre = eDatosFamiliares.EdadDelPadre,
                PaisNacimientoDelPadre = eDatosFamiliares.PaisNacimientoDelPadre,
                ProfesionDelPadre = eDatosFamiliares.ProfesionDelPadre,
                EstadoDeDatosFamiliares = 1
            };

            return View(datosFamiliaresEliminar);
        }


        //Post: Se eliminan datos de familiares asociado al ID de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmadoDatosFamiliares(DatosFamiliaresViewModel DatosFamiliaresEliminados)
        {

            var datosFamiliares = await this._context.DatosFamiliaresDb
                .AsNoTracking().FirstOrDefaultAsync(x => x.per_codigo_id == DatosFamiliaresEliminados.per_codigo_id);

            if (DatosFamiliaresEliminados == null)
            {
                return NotFound();

            }

            try
            {
                await this._familiares.EliminarConfirmadoDatosFamiliares(DatosFamiliaresEliminados);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Personas", "Personas");
        }


        #endregion Area Datos de familiares



        #region Método crea una lista de datos familiares disponibles de la persona.
        public void DatosFamiliaresDisponibles(int idPersona)

        {

            //Se crea una lista utilizando un ViewModel

            List<DatosFamiliaresViewModel> ListDatosFamiliares = new List<DatosFamiliaresViewModel>();


            //Se realiza una consulta a la Db uniendo
            //tabla persona con tabla datos familiares

            ListDatosFamiliares = (from oDatosFamiliares in this._context.DatosFamiliaresDb
                                   join persona in this._context.PersonasDb
                                    on oDatosFamiliares.per_codigo_id equals persona.per_codigo_id
                                   where persona.per_codigo_id == idPersona


                                   select new DatosFamiliaresViewModel

                                   {
                                       per_primer_ape = persona.per_primer_ape,
                                       per_segundo_ape = persona.per_segundo_ape,
                                       per_primer_nom = persona.per_primer_nom,
                                       per_segundo_nom = persona.per_segundo_nom,                                      
                                       per_codigo_id = oDatosFamiliares.per_codigo_id,
                                       Id_DatosFamiliares = oDatosFamiliares.Id_DatosFamiliares,                                      
                                       PrimerNombreDeLaMadre = oDatosFamiliares.PrimerNombreDeLaMadre,
                                       SegundoNombreDeLaMadre = oDatosFamiliares.SegundoNombreDeLaMadre,
                                       ApellidosDeLaMadre = oDatosFamiliares.ApellidosDeLaMadre,
                                       FechaNacimientoDeLaMadre = oDatosFamiliares.FechaNacimientoDeLaMadre,
                                       EdadDeLaMadre = oDatosFamiliares.EdadDeLaMadre,
                                       PaisNacimientoDeLaMadre = oDatosFamiliares.PaisNacimientoDeLaMadre,
                                       ProfesionDeLaMadre = oDatosFamiliares.ProfesionDeLaMadre,

                                       PrimerNombreDelPadre = oDatosFamiliares.PrimerNombreDelPadre,
                                       SegundoNombreDelPadre = oDatosFamiliares.SegundoNombreDelPadre,
                                       ApellidosDelPadre = oDatosFamiliares.ApellidosDelPadre,
                                       FechaNacimientoDelPadre = oDatosFamiliares.FechaNacimientoDelPadre,
                                       EdadDelPadre = oDatosFamiliares.EdadDelPadre,
                                       PaisNacimientoDelPadre = oDatosFamiliares.PaisNacimientoDelPadre,
                                       ProfesionDelPadre = oDatosFamiliares.ProfesionDelPadre,
                                      
                                   }).ToList();

            ViewBag.ListDatosFam = ListDatosFamiliares;

        }
        #endregion

        #region Mètodo crea lista con los nombres de la persona seleccionada.
        //Crea una lista con nombres de la persona
        public void PersonaSeleccionada(int IdPersona)

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
