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


        #region Método Exportar Datos Familiares a Excel.

        public static List<DatosFamiliaresViewModel> DatosDeFamiliaExcel;


        public FileResult ExportarDatosFamiliaresExcel(string[] nombrePropiedades)
        {
            byte[] buffer = ExportarExcelGeneric(nombrePropiedades, DatosDeFamiliaExcel);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        #endregion Método Exportar Documentos a Excel


        #region Mètodo crea lista con los nombres de la persona seleccionada.
        //Crea una lista con nombres de la persona
        public void NombrePersonaSeleccionada(int IdPersona)

        {
            List<PersonaDTO> ListNombres = new List<PersonaDTO>();

            ListNombres = (from p in this._context.PersonasDb
                           where p.per_codigo_id == IdPersona

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
        #endregion Mètodo crea lista con los nombres de la persona seleccionada.


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


        #region Area datos de familiares


        //Get: Se agregan datos de familiares asociado al ID de la persona
        [HttpGet]
        public IActionResult CrearDatosFamiliares(int? id)

        {
            //Obtenemos el ID de la persona.
            var PersonDatoFam = this._context.PersonasDb
                .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonDatoFam.per_codigo_id;

            DatosFamiliaresDisponibles(PersonDatoFam.per_codigo_id);
            NombrePersonaSeleccionada(PersonDatoFam.per_codigo_id);

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

            //Obtenemos ID de la persona.
            var PersonaDatosFamiliales = this._context.PersonasDb.AsNoTracking()
                .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonaDatosFamiliales.per_codigo_id;

            DatosFamiliaresDisponibles(PersonaDatosFamiliales.per_codigo_id);
            NombrePersonaSeleccionada(PersonaDatosFamiliales.per_codigo_id);

            //Obtenemos datos familiares de la persona.
            var DatosFamiliares = this._context.DatosFamiliaresDb.AsNoTracking()
                .FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdDatosFamiliares = DatosFamiliares;

            //Si no tiene datos familiares la persona, se llena una alerta.
            if (ViewBag.IdDatosFamiliares == null)
            {
                TempData["alertaDatosFamiliares1"] = "La persona seleccionada";
                TempData["alertaDatosFamiliares2"] = "no posee datos familiares, desea agregarlos?";
            }


            List<DatosFamiliaresViewModel> DatosFamiliares_ExportarExcel = new List<DatosFamiliaresViewModel>();

            DatosFamiliares_ExportarExcel = (from df in this._context.DatosFamiliaresDb
                                             join p in this._context.PersonasDb
                                             on df.per_codigo_id equals p.per_codigo_id
                                             where p.per_codigo_id == id

                                             select new DatosFamiliaresViewModel
                                             {
                                                 per_codigo_id = df.per_codigo_id,
                                                 Id_DatosFamiliares = df.Id_DatosFamiliares,
                                                 PrimerNombreDeLaMadre = df.PrimerNombreDeLaMadre,
                                                 SegundoNombreDeLaMadre = df.SegundoNombreDeLaMadre,
                                                 ApellidosDeLaMadre = df.ApellidosDeLaMadre,
                                                 FechaNacimientoDeLaMadre = df.FechaNacimientoDeLaMadre,
                                                 EdadDeLaMadre = df.EdadDeLaMadre,
                                                 PaisNacimientoDeLaMadre = df.PaisNacimientoDeLaMadre,
                                                 ProfesionDeLaMadre = df.ProfesionDeLaMadre,
                                                 PrimerNombreDelPadre = df.PrimerNombreDelPadre,
                                                 SegundoNombreDelPadre = df.SegundoNombreDelPadre,
                                                 ApellidosDelPadre = df.ApellidosDelPadre,
                                                 FechaNacimientoDelPadre = df.FechaNacimientoDelPadre,
                                                 EdadDelPadre = df.EdadDelPadre,
                                                 PaisNacimientoDelPadre = df.PaisNacimientoDelPadre,
                                                 ProfesionDelPadre = df.ProfesionDelPadre,
                                                 EstadoDeDatosFamiliares = 1,

                                             }).ToList();

            DatosDeFamiliaExcel = DatosFamiliares_ExportarExcel;

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

            var eDatosFamiliares = await this._context.DatosFamiliaresDb.AsNoTracking()
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
                FechaNacimientoDeLaMadre = eDatosFamiliares.FechaNacimientoDeLaMadre,
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


        #endregion Area datos de familiares

    }
}
