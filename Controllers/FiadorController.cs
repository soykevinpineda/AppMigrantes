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



        [HttpGet]
        public IActionResult CrearFiador(int? id)
        {

            var Fiador_Persona = this._context.FiadorDb
           .FirstOrDefault(x => x.per_codigo_id == id);


            if (Fiador_Persona == null)
            {

                TempData["alertaFiador"] = "La persona no tiene un fiador, desea crear uno?";

            }

            ViewBag.FiadorPersona = Fiador_Persona;

            var Codigo_Persona = this._context.PersonasDb
            .FirstOrDefault(x => x.per_codigo_id == id);

            ViewBag.IdPersona = Codigo_Persona.per_codigo_id;

            ListFiador(Codigo_Persona.per_codigo_id);

            Sexos();


            var NombrePersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_primer_nom).FirstOrDefault();
            ViewBag.Nombre_Persona = NombrePersona;

            var ApellidoPersona = this._context.PersonasDb
            .Where(x => x.per_codigo_id == id)
            .Select(x => x.per_primer_ape).FirstOrDefault();
            ViewBag.Apellido_Persona = ApellidoPersona;

            return View();
        }



        //POST: Guardando al fiador asociado a la persona.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuardarFiador(FiadorViewModel FiadorGuardado)
        {


            Sexos();

            if (ModelState.IsValid)
            {
                try
                {
                    await this._fiador.GuardarFiador(FiadorGuardado);
                }
                catch (Exception)
                {
                    throw;
                }


            }
            return RedirectToAction("Personas", "Personas");


        }



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
