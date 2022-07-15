using Migrantes.Models.DTO;
using Migrantes.Models.Entities;
using Migrantes.ViewModels;
using System;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Familiares
{
    public class Familiares : IFamiliares
    {

        //Declarando ApplicationDbContext
        private readonly ApplicationDbContext _context;

        public Familiares(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task AgregandoDatosFamiliares(DatosFamiliaresDTO oDatosFamiliaresCreados)
        {
            var EdadDelFamiliar = DateTime.Now.Year - oDatosFamiliaresCreados.FechaNacimientoDelFamiliar.Year;
           
            DatosFamiliares ObjFamiliares = new DatosFamiliares();

            ObjFamiliares.DatosFamiliaresID = oDatosFamiliaresCreados.DatosFamiliaresID;
            ObjFamiliares.per_codigo_id = oDatosFamiliaresCreados.per_codigo_id;
            ObjFamiliares.ParienteID = oDatosFamiliaresCreados.ParienteID;
            ObjFamiliares.PrimerNombreFamiliar = oDatosFamiliaresCreados.PrimerNombreFamiliar;
            ObjFamiliares.SegundoNombreFamiliar = oDatosFamiliaresCreados.SegundoNombreFamiliar;
            ObjFamiliares.ApellidosFamiliar = oDatosFamiliaresCreados.ApellidosFamiliar;
            ObjFamiliares.FechaNacimientoDelFamiliar = oDatosFamiliaresCreados.FechaNacimientoDelFamiliar;
            ObjFamiliares.PaisNacimientoDelFamiliar = oDatosFamiliaresCreados.PaisNacimientoDelFamiliar;
            ObjFamiliares.EdadDelFamiliar = EdadDelFamiliar;
            ObjFamiliares.TelefonoDelFamiliar = oDatosFamiliaresCreados.TelefonoDelFamiliar;
            ObjFamiliares.EmaiDelFamiliar = oDatosFamiliaresCreados.EmaiDelFamiliar;
            ObjFamiliares.ProfesionDelFamiliar = oDatosFamiliaresCreados.ProfesionDelFamiliar;
            ObjFamiliares.EstadoDatosFamiliares = 1;

            this._context.DatosFamiliaresDb.Add(ObjFamiliares);
            await this._context.SaveChangesAsync();

        }

        public async Task ActualizarFamiliaresEditados(DatosFamiliaresDTO DatosFamiliaresEditados)
        {
            var EdadDelFamiliar = DateTime.Now.Year - DatosFamiliaresEditados.FechaNacimientoDelFamiliar.Year;


            DatosFamiliares oDatosFamiliares = new DatosFamiliares();

            oDatosFamiliares.DatosFamiliaresID = DatosFamiliaresEditados.DatosFamiliaresID;
            oDatosFamiliares.per_codigo_id = DatosFamiliaresEditados.per_codigo_id;
            oDatosFamiliares.ParienteID = DatosFamiliaresEditados.ParienteID;
            oDatosFamiliares.PrimerNombreFamiliar = DatosFamiliaresEditados.PrimerNombreFamiliar;
            oDatosFamiliares.SegundoNombreFamiliar = DatosFamiliaresEditados.SegundoNombreFamiliar;
            oDatosFamiliares.ApellidosFamiliar = DatosFamiliaresEditados.ApellidosFamiliar;
            oDatosFamiliares.FechaNacimientoDelFamiliar = DatosFamiliaresEditados.FechaNacimientoDelFamiliar;
            oDatosFamiliares.PaisNacimientoDelFamiliar = DatosFamiliaresEditados.PaisNacimientoDelFamiliar;
            oDatosFamiliares.EdadDelFamiliar = EdadDelFamiliar;
            oDatosFamiliares.TelefonoDelFamiliar = DatosFamiliaresEditados.TelefonoDelFamiliar;
            oDatosFamiliares.EmaiDelFamiliar = DatosFamiliaresEditados.EmaiDelFamiliar;
            oDatosFamiliares.ProfesionDelFamiliar = DatosFamiliaresEditados.ProfesionDelFamiliar;
            oDatosFamiliares.EstadoDatosFamiliares = 1;

            this._context.DatosFamiliaresDb.Update(oDatosFamiliares);
            await this._context.SaveChangesAsync();

        }

        public async Task EliminarConfirmadoDatosFamiliares(DatosFamiliaresDTO DatosFamiliaresEliminados)
        {

            DatosFamiliares ObjFamiliares = new DatosFamiliares();

            ObjFamiliares.DatosFamiliaresID = DatosFamiliaresEliminados.DatosFamiliaresID;
            ObjFamiliares.per_codigo_id = DatosFamiliaresEliminados.per_codigo_id;
            ObjFamiliares.ParienteID = DatosFamiliaresEliminados.ParienteID;
            ObjFamiliares.PrimerNombreFamiliar = DatosFamiliaresEliminados.PrimerNombreFamiliar;
            ObjFamiliares.SegundoNombreFamiliar = DatosFamiliaresEliminados.SegundoNombreFamiliar;
            ObjFamiliares.ApellidosFamiliar = DatosFamiliaresEliminados.ApellidosFamiliar;
            ObjFamiliares.FechaNacimientoDelFamiliar = DatosFamiliaresEliminados.FechaNacimientoDelFamiliar;
            ObjFamiliares.PaisNacimientoDelFamiliar = DatosFamiliaresEliminados.PaisNacimientoDelFamiliar;
            ObjFamiliares.EdadDelFamiliar = DatosFamiliaresEliminados.EdadDelFamiliar;
            ObjFamiliares.TelefonoDelFamiliar = DatosFamiliaresEliminados.TelefonoDelFamiliar;
            ObjFamiliares.EmaiDelFamiliar = DatosFamiliaresEliminados.EmaiDelFamiliar;
            ObjFamiliares.ProfesionDelFamiliar = DatosFamiliaresEliminados.ProfesionDelFamiliar;
            ObjFamiliares.EstadoDatosFamiliares = 1;

            this._context.DatosFamiliaresDb.Remove(ObjFamiliares);
            await this._context.SaveChangesAsync();

        }

    }
}
