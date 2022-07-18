using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public static List<DatosFamiliaresDTO> DatosDeFamiliaExcel;


        public FileResult ExportarDatosFamiliaresExcel(string[] nombrePropiedades)
        {
            byte[] buffer = ExportarExcelGeneric(nombrePropiedades, DatosDeFamiliaExcel);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        #endregion Método Exportar Documentos a Excel


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


        #region Método crea una lista de datos familiares disponibles de la persona.
        public void DatosFamiliaresDisponibles(int? PersonaID)
        {

            //Se crea una lista utilizando un ViewModel
            List<DatosFamiliaresDTO> ListDatosFamiliares = new List<DatosFamiliaresDTO>();


            //Se realiza una consulta a la Db uniendo
            //tabla Persona con tabla Datos Familiares.

            ListDatosFamiliares = (from oDatosFamiliares in this._context.DatosFamiliaresDb
                                   join persona in this._context.PersonasDb
                                   on oDatosFamiliares.per_codigo_id equals persona.per_codigo_id
                                   join pariente in this._context.ParientesDb
                                   on oDatosFamiliares.ParienteID equals pariente.ParienteID
                                   where oDatosFamiliares.per_codigo_id == PersonaID

                                   select new DatosFamiliaresDTO

                                   {
                                       per_primer_ape = persona.per_primer_ape,
                                       per_segundo_ape = persona.per_segundo_ape,
                                       per_primer_nom = persona.per_primer_nom,
                                       per_segundo_nom = persona.per_segundo_nom,
                                       per_codigo_id = oDatosFamiliares.per_codigo_id,
                                       DatosFamiliaresID = oDatosFamiliares.DatosFamiliaresID,
                                       ParienteID= oDatosFamiliares.ParienteID,
                                       DescripcionDelPariente = pariente.DescripcionPariente,
                                       PrimerNombreFamiliar = oDatosFamiliares.PrimerNombreFamiliar,
                                       SegundoNombreFamiliar = oDatosFamiliares.SegundoNombreFamiliar,
                                       ApellidosFamiliar = oDatosFamiliares.ApellidosFamiliar,
                                       FechaNacimientoDelFamiliar = oDatosFamiliares.FechaNacimientoDelFamiliar,
                                       PaisNacimientoDelFamiliar = oDatosFamiliares.PaisNacimientoDelFamiliar,
                                       EdadDelFamiliar = oDatosFamiliares.EdadDelFamiliar,
                                       TelefonoDelFamiliar = oDatosFamiliares.TelefonoDelFamiliar,
                                       EmaiDelFamiliar = oDatosFamiliares.EmaiDelFamiliar,
                                       ProfesionDelFamiliar = oDatosFamiliares.ProfesionDelFamiliar,

                                   }).ToList();

            ViewBag.ListDatosFam = ListDatosFamiliares;
            
        }
        #endregion


        public async Task ObtenerPersona(int? id)
        {
            var GetPerson = await this._context.PersonasDb
                .FirstOrDefaultAsync(x => x.per_codigo_id == id);

            ViewBag.PersonaID = GetPerson.per_codigo_id;
        }


        public async Task ObtenerFamiliar(int? id)
        {

            var GetFamily = await this._context.DatosFamiliaresDb
                .FirstOrDefaultAsync(x => x.DatosFamiliaresID == id);

            ViewBag.DatosFamiliaresID = GetFamily.DatosFamiliaresID;
        }

        #region Area datos de familiares


        //Get: Se agregan datos de familiares asociado al ID de la persona
        [HttpGet]
        public async Task<IActionResult> CrearDatosFamiliares(int? id)

        {
            await ObtenerPersona(id);

            if (id == null)
            {
                return NotFound();
            }

            Parientes();
            DatosFamiliaresDisponibles(id);
            NombrePersonaSeleccionada(id);

            return await Task.Run(() => View());

        }


        //Post: Se guardan datos de familiares asociado al ID de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregandoDatosFamiliares(DatosFamiliaresDTO DatosFamiliaresAgregados)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("CrearPersona", "Personas");
                }
                else
                {
                    await this._familiares.AgregandoDatosFamiliares(DatosFamiliaresAgregados);
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
        public async Task<IActionResult> DetallesDatosFamiliares(int? id)
        {

            //Obtenemos la ruta de inicio del usuario.
            var urlRetornoDetallesDelFamiliar = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno", urlRetornoDetallesDelFamiliar);

            if (id == null)
            {
                return NotFound();
            }

            //await ObtenerFamiliar(id);

            var ObtenerFamiliar = await this._context.DatosFamiliaresDb
               .FirstOrDefaultAsync(x => x.DatosFamiliaresID == id);

            ViewBag.DatosFamiliaresID = ObtenerFamiliar;

            var Pariente = new DatosFamiliaresDTO()
            {
                per_codigo_id = ObtenerFamiliar.per_codigo_id,
                DatosFamiliaresID = ObtenerFamiliar.DatosFamiliaresID,
                ParienteID = ObtenerFamiliar.ParienteID,
                PrimerNombreFamiliar = ObtenerFamiliar.PrimerNombreFamiliar,
                SegundoNombreFamiliar = ObtenerFamiliar.SegundoNombreFamiliar,
                ApellidosFamiliar = ObtenerFamiliar.ApellidosFamiliar,
                FechaNacimientoDelFamiliar = ObtenerFamiliar.FechaNacimientoDelFamiliar,
                PaisNacimientoDelFamiliar = ObtenerFamiliar.PaisNacimientoDelFamiliar,
                EdadDelFamiliar = ObtenerFamiliar.EdadDelFamiliar,
                TelefonoDelFamiliar = ObtenerFamiliar.TelefonoDelFamiliar,
                EmaiDelFamiliar = ObtenerFamiliar.EmaiDelFamiliar,
                ProfesionDelFamiliar = ObtenerFamiliar.ProfesionDelFamiliar,
                EstadoDatosFamiliares = 1
            };

            Parientes();
            NombrePersonaSeleccionada(ObtenerFamiliar.per_codigo_id); 

            return await Task.Run(() => View(Pariente));
        }


        //Get: Se editan datos de familiares asociado al ID de la persona
        [HttpGet]
        public async Task<IActionResult> EditarDatosFamiliares(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var ObtenerFamiliar = await this._context.DatosFamiliaresDb
               .FirstOrDefaultAsync(x => x.DatosFamiliaresID == id);

            ViewBag.DatosFamiliaresID = ObtenerFamiliar;

            var EditarFamiliar = new DatosFamiliaresDTO()
            {
                per_codigo_id = ObtenerFamiliar.per_codigo_id,
                DatosFamiliaresID = ObtenerFamiliar.DatosFamiliaresID,
                ParienteID = ObtenerFamiliar.ParienteID,
                PrimerNombreFamiliar = ObtenerFamiliar.PrimerNombreFamiliar,
                SegundoNombreFamiliar = ObtenerFamiliar.SegundoNombreFamiliar,
                ApellidosFamiliar = ObtenerFamiliar.ApellidosFamiliar,
                FechaNacimientoDelFamiliar = ObtenerFamiliar.FechaNacimientoDelFamiliar,
                PaisNacimientoDelFamiliar = ObtenerFamiliar.PaisNacimientoDelFamiliar,
                EdadDelFamiliar = ObtenerFamiliar.EdadDelFamiliar,
                TelefonoDelFamiliar = ObtenerFamiliar.TelefonoDelFamiliar,
                EmaiDelFamiliar = ObtenerFamiliar.EmaiDelFamiliar,
                ProfesionDelFamiliar = ObtenerFamiliar.ProfesionDelFamiliar,
                EstadoDatosFamiliares = 1
            };

            Parientes();
            DatosFamiliaresDisponibles(id);
            NombrePersonaSeleccionada(ObtenerFamiliar.per_codigo_id);

            return await Task.Run(() => View(EditarFamiliar));
        }


        //Post: Se actualiza datos editados de familiares asociado al ID de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarFamiliaresEditados(DatosFamiliaresDTO DatosFamiliaresEditados)
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

            var urlRetornoDetallesDelFamiliar = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoDetallesDelFamiliar);

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

            var ObtenerFamiliar = await this._context.DatosFamiliaresDb
             .FirstOrDefaultAsync(x => x.DatosFamiliaresID == id);

            var EliminarFamiliar = new DatosFamiliaresDTO()
            {
                per_codigo_id = ObtenerFamiliar.per_codigo_id,
                DatosFamiliaresID = ObtenerFamiliar.DatosFamiliaresID,
                ParienteID = ObtenerFamiliar.ParienteID,
                PrimerNombreFamiliar = ObtenerFamiliar.PrimerNombreFamiliar,
                SegundoNombreFamiliar = ObtenerFamiliar.SegundoNombreFamiliar,
                ApellidosFamiliar = ObtenerFamiliar.ApellidosFamiliar,
                FechaNacimientoDelFamiliar = ObtenerFamiliar.FechaNacimientoDelFamiliar,
                PaisNacimientoDelFamiliar = ObtenerFamiliar.PaisNacimientoDelFamiliar,
                EdadDelFamiliar = ObtenerFamiliar.EdadDelFamiliar,
                TelefonoDelFamiliar = ObtenerFamiliar.TelefonoDelFamiliar,
                EmaiDelFamiliar = ObtenerFamiliar.EmaiDelFamiliar,
                ProfesionDelFamiliar = ObtenerFamiliar.ProfesionDelFamiliar,
                EstadoDatosFamiliares = 1
            };

            Parientes();
            DatosFamiliaresDisponibles(id);
            NombrePersonaSeleccionada(ObtenerFamiliar.per_codigo_id);

            return await Task.Run(() => View(EliminarFamiliar));
        }


        //Post: Se eliminan datos de familiares asociado al ID de la persona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmadoDatosFamiliares(DatosFamiliaresDTO DatosFamiliaresEliminados)
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

            var urlRetornoFamiliaresDisponibles = HttpContext.Session.GetString("UrlRetorno");
            return LocalRedirect(urlRetornoFamiliaresDisponibles);
        }



        [HttpGet]
        public async Task<IActionResult> FamiliaresDisponibles(int? id)
        {
            //Obtenemos la ruta de inicio del usuario.
            var urlRetornoFamiliaresDisponibles = HttpContext.Request.Path + HttpContext.Request.QueryString;
            HttpContext.Session.SetString("UrlRetorno", urlRetornoFamiliaresDisponibles);

            await ObtenerPersona(id);
            await ObtenerFamiliar(id);

            if (id == null)
            {
                return NotFound();
            }

            DatosFamiliaresDisponibles(id);
            NombrePersonaSeleccionada(id);

              var GetFamily = this._context.DatosFamiliaresDb
                   .Count(x => x.per_codigo_id == id);

              ViewBag.DatosFamiliaresID = GetFamily;

            //Si no tiene datos familiares la persona, se llena una alerta.
            if (ViewBag.DatosFamiliaresID == 0)
            {
                TempData["alertaDatosFamiliares1"] = "La persona seleccionada";
                TempData["alertaDatosFamiliares2"] = "no posee datos familiares, desea agregarlos?";
            }

            List<DatosFamiliaresDTO> DatosFamiliares_ExportarExcel = new List<DatosFamiliaresDTO>();

            DatosFamiliares_ExportarExcel = (from df in this._context.DatosFamiliaresDb
                                             join p in this._context.PersonasDb
                                             on df.per_codigo_id equals p.per_codigo_id
                                             join pariente in this._context.ParientesDb
                                             on df.ParienteID equals pariente.ParienteID
                                             where p.per_codigo_id == id

                                             select new DatosFamiliaresDTO
                                             {
                                                 per_codigo_id = df.per_codigo_id,
                                                 DatosFamiliaresID = df.DatosFamiliaresID,
                                                 DescripcionDelPariente = pariente.DescripcionPariente,
                                                 PrimerNombreFamiliar = df.PrimerNombreFamiliar,
                                                 SegundoNombreFamiliar = df.SegundoNombreFamiliar,
                                                 ApellidosFamiliar = df.ApellidosFamiliar,
                                                 FechaNacimientoDelFamiliar = df.FechaNacimientoDelFamiliar,
                                                 PaisNacimientoDelFamiliar = df.PaisNacimientoDelFamiliar,
                                                 EdadDelFamiliar = df.EdadDelFamiliar,
                                                 TelefonoDelFamiliar = df.TelefonoDelFamiliar,
                                                 EmaiDelFamiliar = df.EmaiDelFamiliar,
                                                 ProfesionDelFamiliar = df.ProfesionDelFamiliar,
                                                 EstadoDatosFamiliares = 1

                                             }).ToList();

            DatosDeFamiliaExcel = DatosFamiliares_ExportarExcel;

            return View(DatosFamiliares_ExportarExcel);
        }


        #endregion Area datos de familiares





        //Combo Box: Parientes
        public void Parientes()
        {
            List<SelectListItem> SelectListParientes = new List<SelectListItem>();//Combo Box

            try
            {
                SelectListParientes = (from p in this._context.ParientesDb
                           where p.Estado == 1
                           select new SelectListItem
                           {
                               Text = p.DescripcionPariente,
                               Value = p.ParienteID.ToString()
                           }).ToList();
                SelectListParientes.Insert(0, new SelectListItem
                {
                    Text = "--Seleccione--",
                    Value = ""
                });
            }
            catch (Exception)
            {
                throw;
            }

            ViewBag.Parientes = SelectListParientes;
        }


        //Lista para mostrar en la vista Editar
        public List<SelectListItem> ParientesEditar()
        {
            List<SelectListItem> SelectListParientes = new List<SelectListItem>();//Combo Box

            try
            {
                SelectListParientes = (from p in this._context.ParientesDb
                                       where p.Estado == 1
                                       select new SelectListItem
                                       {
                                           Text = p.DescripcionPariente,
                                           Value = p.ParienteID.ToString()
                                       }).ToList();
                SelectListParientes.Insert(0, new SelectListItem
                {
                    Text = "--Seleccione--",
                    Value = ""
                });
            }
            catch (Exception)
            {
                throw;
            }

            return SelectListParientes;
        }


    }
}
