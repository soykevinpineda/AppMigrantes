using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Migrantes.Data;
using Migrantes.Data.Servicios.Documentos;
using Migrantes.Data.Servicios.Familiares;
using Migrantes.Data.Servicios.Fiador;
using Migrantes.Data.Servicios.Personas;
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
            IDocumentos documentos, IPersonas persona, IFamiliares agregandofamiliares,IFiador fiador)
        {
            this._fiador = fiador;
            this._documentos = documentos;
            this._persona = persona;
            this._familiares = agregandofamiliares;
            this._context = context;
        }




        [HttpGet]
        public  IActionResult CrearFiador()
        {
         

            Sexos();
            return View();
        }

        //POST: Guardando al fiador asociado a la persona.
        [HttpPost]
        public async Task<ActionResult> GuardarFiador(int? id,FiadorViewModel FiadorGuardado)
        {
 
            Sexos();

            if (!ModelState.IsValid)
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
