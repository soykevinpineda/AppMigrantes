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

        public async Task AgregandoDatosFamiliares(DatosFamiliaresViewModel oDatosFamiliaresCreados)
        {
            var edadMadre= DateTime.Now.Year - oDatosFamiliaresCreados.FechaNacimientoDeLaMadre.Year;
            var edadPadre = DateTime.Now.Year - oDatosFamiliaresCreados.FechaNacimientoDelPadre.Year;

            DatosFamiliares ObjFamiliares = new DatosFamiliares();

            ObjFamiliares.Id_DatosFamiliares = oDatosFamiliaresCreados.Id_DatosFamiliares;
            ObjFamiliares.per_codigo_id = oDatosFamiliaresCreados.per_codigo_id;
            ObjFamiliares.PrimerNombreDeLaMadre = oDatosFamiliaresCreados.PrimerNombreDeLaMadre;
            ObjFamiliares.SegundoNombreDeLaMadre = oDatosFamiliaresCreados.SegundoNombreDeLaMadre;
            ObjFamiliares.ApellidosDeLaMadre = oDatosFamiliaresCreados.ApellidosDeLaMadre;
            ObjFamiliares.FechaNacimientoDeLaMadre = oDatosFamiliaresCreados.FechaNacimientoDeLaMadre;
            ObjFamiliares.EdadDeLaMadre = edadMadre;
            ObjFamiliares.PaisNacimientoDeLaMadre = oDatosFamiliaresCreados.PaisNacimientoDeLaMadre;
            ObjFamiliares.ProfesionDeLaMadre = oDatosFamiliaresCreados.ProfesionDeLaMadre;

            ObjFamiliares.PrimerNombreDelPadre = oDatosFamiliaresCreados.PrimerNombreDelPadre;
            ObjFamiliares.SegundoNombreDelPadre = oDatosFamiliaresCreados.SegundoNombreDelPadre;
            ObjFamiliares.ApellidosDelPadre = oDatosFamiliaresCreados.ApellidosDelPadre;
            ObjFamiliares.FechaNacimientoDelPadre = oDatosFamiliaresCreados.FechaNacimientoDelPadre;
            ObjFamiliares.EdadDelPadre = edadPadre;
            ObjFamiliares.PaisNacimientoDelPadre = oDatosFamiliaresCreados.PaisNacimientoDelPadre;
            ObjFamiliares.ProfesionDelPadre = oDatosFamiliaresCreados.ProfesionDelPadre;
            ObjFamiliares.EstadoDeDatosFamiliares = 1;

            this._context.DatosFamiliaresDb.Add(ObjFamiliares);
            await this._context.SaveChangesAsync();

        }

        public async Task ActualizarFamiliaresEditados(DatosFamiliaresViewModel DatosFamiliaresEditados)
        {
            var edadMadre = DateTime.Now.Year - DatosFamiliaresEditados.FechaNacimientoDeLaMadre.Year;
            var edadPadre = DateTime.Now.Year - DatosFamiliaresEditados.FechaNacimientoDelPadre.Year;

            DatosFamiliares oDatosFamiliares = new DatosFamiliares();

            oDatosFamiliares.Id_DatosFamiliares = DatosFamiliaresEditados.Id_DatosFamiliares;
            oDatosFamiliares.per_codigo_id = DatosFamiliaresEditados.per_codigo_id;
            oDatosFamiliares.PrimerNombreDeLaMadre = DatosFamiliaresEditados.PrimerNombreDeLaMadre;
            oDatosFamiliares.SegundoNombreDeLaMadre = DatosFamiliaresEditados.SegundoNombreDeLaMadre;
            oDatosFamiliares.ApellidosDeLaMadre = DatosFamiliaresEditados.ApellidosDeLaMadre;
            oDatosFamiliares.FechaNacimientoDeLaMadre = DatosFamiliaresEditados.FechaNacimientoDeLaMadre;
            oDatosFamiliares.EdadDeLaMadre = edadMadre;
            oDatosFamiliares.PaisNacimientoDeLaMadre = DatosFamiliaresEditados.PaisNacimientoDeLaMadre;
            oDatosFamiliares.ProfesionDeLaMadre = DatosFamiliaresEditados.ProfesionDeLaMadre;

            oDatosFamiliares.PrimerNombreDelPadre = DatosFamiliaresEditados.PrimerNombreDelPadre;
            oDatosFamiliares.SegundoNombreDelPadre = DatosFamiliaresEditados.SegundoNombreDelPadre;
            oDatosFamiliares.ApellidosDelPadre = DatosFamiliaresEditados.ApellidosDelPadre;
            oDatosFamiliares.FechaNacimientoDelPadre = DatosFamiliaresEditados.FechaNacimientoDelPadre;
            oDatosFamiliares.EdadDelPadre = edadPadre;
            oDatosFamiliares.PaisNacimientoDelPadre = DatosFamiliaresEditados.PaisNacimientoDelPadre;
            oDatosFamiliares.ProfesionDelPadre = DatosFamiliaresEditados.ProfesionDelPadre;
            oDatosFamiliares.EstadoDeDatosFamiliares = 1;

            this._context.DatosFamiliaresDb.Update(oDatosFamiliares);
            await this._context.SaveChangesAsync();

        }

        public async Task EliminarConfirmadoDatosFamiliares(DatosFamiliaresViewModel DatosFamiliaresEliminados)

        {

            DatosFamiliares ObjFamiliares = new DatosFamiliares();

            ObjFamiliares.Id_DatosFamiliares = DatosFamiliaresEliminados.Id_DatosFamiliares;
            ObjFamiliares.per_codigo_id = DatosFamiliaresEliminados.per_codigo_id;
            ObjFamiliares.PrimerNombreDeLaMadre = DatosFamiliaresEliminados.PrimerNombreDeLaMadre;
            ObjFamiliares.SegundoNombreDeLaMadre = DatosFamiliaresEliminados.SegundoNombreDeLaMadre;
            ObjFamiliares.ApellidosDeLaMadre = DatosFamiliaresEliminados.ApellidosDeLaMadre;
            ObjFamiliares.FechaNacimientoDeLaMadre = DatosFamiliaresEliminados.FechaNacimientoDeLaMadre;
            ObjFamiliares.EdadDeLaMadre = DatosFamiliaresEliminados.EdadDeLaMadre;
            ObjFamiliares.PaisNacimientoDeLaMadre = DatosFamiliaresEliminados.PaisNacimientoDeLaMadre;
            ObjFamiliares.ProfesionDeLaMadre = DatosFamiliaresEliminados.ProfesionDeLaMadre;

            ObjFamiliares.PrimerNombreDelPadre = DatosFamiliaresEliminados.PrimerNombreDelPadre;
            ObjFamiliares.SegundoNombreDelPadre = DatosFamiliaresEliminados.SegundoNombreDelPadre;
            ObjFamiliares.ApellidosDelPadre = DatosFamiliaresEliminados.ApellidosDelPadre;
            ObjFamiliares.FechaNacimientoDelPadre = DatosFamiliaresEliminados.FechaNacimientoDelPadre;
            ObjFamiliares.EdadDelPadre = DatosFamiliaresEliminados.EdadDelPadre;
            ObjFamiliares.PaisNacimientoDelPadre = DatosFamiliaresEliminados.PaisNacimientoDelPadre;
            ObjFamiliares.ProfesionDelPadre = DatosFamiliaresEliminados.ProfesionDelPadre;
            ObjFamiliares.EstadoDeDatosFamiliares = 1;

            this._context.DatosFamiliaresDb.Remove(ObjFamiliares);
            await this._context.SaveChangesAsync();

        }

    }
}
