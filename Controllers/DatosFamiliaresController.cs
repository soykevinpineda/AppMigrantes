using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migrantes.Data;
using Migrantes.Data.Servicios.Documentos;
using Migrantes.Data.Servicios.Familiares;
using Migrantes.Data.Servicios.Personas;
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

        //Aqui se van agregando las Interfaces creadas.
        public DatosFamiliaresController(ApplicationDbContext context,
            IDocumentos documentos, IPersonas persona, IFamiliares agregandofamiliares)
        {
           
            this._documentos = documentos;
            this._persona = persona;
            this._familiares = agregandofamiliares;
            this._context = context;
        }


        #region Area Datos de familiares

        [HttpGet]
        public IActionResult AgregarDatosFamiliares(int? id)

        {
            var PersonaDatoFam = this._context.PersonasDb.FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonaDatoFam.per_codigo_id;

            var ValidacionDatosFamiliares = this._context.DatosFamiliaresDb.FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdDatosFam = ValidacionDatosFamiliares;

            if (ValidacionDatosFamiliares == null)
            {
                TempData["msjValidacionFamiliar"] = "La persona no tiene datos familiares agregados...";
            }

            DatosFamiliaresDisponibles(PersonaDatoFam.per_codigo_id);
    

            return View();

        }


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

            return RedirectToAction("Personas", "Personas");
        }



        [HttpGet]
        public IActionResult DetallesDatosFamiliares(int? id)

        {
            var PersonaDatoFam = this._context.PersonasDb.FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdPersona = PersonaDatoFam.per_codigo_id;

            var ValidacionDatosFamiliares = this._context.DatosFamiliaresDb.FirstOrDefault(x => x.per_codigo_id == id);
            ViewBag.IdDatosFam = ValidacionDatosFamiliares;

            DatosFamiliaresDisponibles(PersonaDatoFam.per_codigo_id);
      
            return View();

        }

        #endregion Area Datos de familiares


        [HttpGet]
        public IActionResult EditarDatosFamiliares (int? id)

        {

            var ObjDatosFamiliares = this._context.DatosFamiliaresDb.FirstOrDefault(x => x.per_codigo_id == id);

            DatosFamiliaresDisponibles(ObjDatosFamiliares.per_codigo_id);

            return View(ObjDatosFamiliares);

        }


        [HttpPost]
        public async Task<IActionResult> ActualizarFamiliaresEditados(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
        
                    try
                    {
                        if (!ModelState.IsValid)
                        {
                            return RedirectToAction("CrearPersona", "Personas");
                        }
                        else
                        {
                            await this._persona.GuardarPersona();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }



            }

            var ObjDatosFamiliares = this._context.DatosFamiliaresDb.FirstOrDefaultAsync(x => x.per_codigo_id == id);

        

            return View(ObjDatosFamiliares);

        }





















        #region Método crea una lista de datos familiares disponibles de la persona.
        public void DatosFamiliaresDisponibles(int idPersona)

        {

            List<DatosFamiliaresViewModel> ListDatosFamiliares = new List<DatosFamiliaresViewModel>();

            ListDatosFamiliares = (from oDatosFam in this._context.DatosFamiliaresDb
                                   join persona in this._context.PersonasDb
                              on oDatosFam.per_codigo_id equals persona.per_codigo_id
                                   where persona.per_codigo_id == idPersona


                                   select new DatosFamiliaresViewModel

                                   {
                                       per_codigo_id = oDatosFam.per_codigo_id,
                                       nombres_madre = oDatosFam.nombres_madre,
                                       primer_apellido_madre = oDatosFam.primer_apellido_madre,
                                       segundo_apellido_madre = oDatosFam.segundo_apellido_madre,
                                       edad_madre = oDatosFam.edad_madre,
                                       profesion_madre = oDatosFam.profesion_madre,

                                       nombres_padre = oDatosFam.nombres_padre,
                                       primer_apellido_padre = oDatosFam.primer_apellido_padre,
                                       segundo_apellido_padre = oDatosFam.segundo_apellido_padre,
                                       edad_padre = oDatosFam.edad_padre,
                                       profesion_padre = oDatosFam.profesion_padre,

                                   }).ToList();

            ViewBag.ListDatosFam = ListDatosFamiliares;

        }
        #endregion



    }
}
