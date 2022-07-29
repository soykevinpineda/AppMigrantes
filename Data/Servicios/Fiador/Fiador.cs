using Migrantes.Models.Entities;
using Migrantes.ViewModels;
using System;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Fiador
{
    public class Fiador : IFiador
    {

        //Declarando ApplicationDbContext
        private readonly ApplicationDbContext _context;

        public Fiador(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task GuardarFiador(FiadorViewModel FiadorCreado)
        {
            var edadFiador = DateTime.Now.Year - FiadorCreado.FechaNacimientoDelFiador.Year;


            ModeloFiador objFiador = new ModeloFiador();


            if (FiadorCreado.PaisNacimientoDelFiador == null)
            {
                objFiador.PaisNacimientoDelFiador = "N/A";
            }
            else
            {
                objFiador.PaisNacimientoDelFiador = FiadorCreado.PaisNacimientoDelFiador;
            }

            if (FiadorCreado.TelefonoAlternoFiador == null)
            {
                objFiador.TelefonoAlternoFiador = "N/A";
            }
            else
            {
                objFiador.TelefonoAlternoFiador = FiadorCreado.TelefonoAlternoFiador;
            }

            objFiador.per_codigo_id = FiadorCreado.per_codigo_id;
            objFiador.FiadorID = FiadorCreado.FiadorID;
            objFiador.PrimerNombreDelFiador = FiadorCreado.PrimerNombreDelFiador;
            objFiador.SegundoNombreDelFiador = FiadorCreado.SegundoNombreDelFiador;
            objFiador.ApellidosDelFiador = FiadorCreado.ApellidosDelFiador;
            objFiador.FechaNacimientoDelFiador = FiadorCreado.FechaNacimientoDelFiador;
            objFiador.EdadDelFiador = edadFiador;
            objFiador.SexoDelFiador = FiadorCreado.SexoDelFiador;    
            objFiador.EmailFiador = FiadorCreado.EmailFiador;
            objFiador.TelefonoFiador = FiadorCreado.TelefonoFiador;         
            objFiador.EntregoRecibo_Agua_o_Luz = FiadorCreado.EntregoRecibo_Agua_o_Luz;
            objFiador.NumCartasPersonales = FiadorCreado.NumCartasPersonales;
            objFiador.NumCartasFamiliares = FiadorCreado.NumCartasFamiliares;

            this._context.FiadorDb.Add(objFiador);
            await this._context.SaveChangesAsync();
        }

        public async Task GuardarFiadorEditado(FiadorViewModel oFiadorEditado)
        {
            var edadFiador = DateTime.Now.Year - oFiadorEditado.FechaNacimientoDelFiador.Year;

            ModeloFiador objFiadorEditado = new ModeloFiador();


            if (oFiadorEditado.PaisNacimientoDelFiador == null)
            {
                objFiadorEditado.PaisNacimientoDelFiador = "N/A";
            }
            else
            {
                objFiadorEditado.PaisNacimientoDelFiador = oFiadorEditado.PaisNacimientoDelFiador;
            }

            if (oFiadorEditado.TelefonoAlternoFiador == null)
            {
                objFiadorEditado.TelefonoAlternoFiador = "N/A";
            }
            else
            {
                objFiadorEditado.TelefonoAlternoFiador = oFiadorEditado.TelefonoAlternoFiador;
            }

            objFiadorEditado.per_codigo_id = oFiadorEditado.per_codigo_id;
            objFiadorEditado.FiadorID = oFiadorEditado.FiadorID;
            objFiadorEditado.PrimerNombreDelFiador = oFiadorEditado.PrimerNombreDelFiador;
            objFiadorEditado.SegundoNombreDelFiador = oFiadorEditado.SegundoNombreDelFiador;
            objFiadorEditado.ApellidosDelFiador = oFiadorEditado.ApellidosDelFiador;
            objFiadorEditado.FechaNacimientoDelFiador = oFiadorEditado.FechaNacimientoDelFiador;
            objFiadorEditado.EdadDelFiador = edadFiador;
            objFiadorEditado.SexoDelFiador = oFiadorEditado.SexoDelFiador;
            objFiadorEditado.EmailFiador = oFiadorEditado.EmailFiador;
            objFiadorEditado.TelefonoFiador = oFiadorEditado.TelefonoFiador;        
            objFiadorEditado.EntregoRecibo_Agua_o_Luz = oFiadorEditado.EntregoRecibo_Agua_o_Luz;
            objFiadorEditado.NumCartasPersonales = oFiadorEditado.NumCartasPersonales;
            objFiadorEditado.NumCartasFamiliares = oFiadorEditado.NumCartasFamiliares;

            this._context.FiadorDb.Update(objFiadorEditado);
            await this._context.SaveChangesAsync();

        }


        public async Task EliminarFiadorConfirmado(FiadorViewModel FiadorEliminado)
        {

            ModeloFiador objFiadorEliminado = new ModeloFiador();

            objFiadorEliminado.per_codigo_id = FiadorEliminado.per_codigo_id;
            objFiadorEliminado.FiadorID = FiadorEliminado.FiadorID;
            objFiadorEliminado.PrimerNombreDelFiador = FiadorEliminado.PrimerNombreDelFiador;
            objFiadorEliminado.SegundoNombreDelFiador = FiadorEliminado.SegundoNombreDelFiador;
            objFiadorEliminado.ApellidosDelFiador = FiadorEliminado.ApellidosDelFiador;
            objFiadorEliminado.FechaNacimientoDelFiador = FiadorEliminado.FechaNacimientoDelFiador;
            objFiadorEliminado.EdadDelFiador = FiadorEliminado.EdadDelFiador;
            objFiadorEliminado.SexoDelFiador = FiadorEliminado.SexoDelFiador;
            objFiadorEliminado.PaisNacimientoDelFiador = FiadorEliminado.PaisNacimientoDelFiador;
            objFiadorEliminado.EmailFiador = FiadorEliminado.EmailFiador;
            objFiadorEliminado.TelefonoFiador = FiadorEliminado.TelefonoFiador;
            objFiadorEliminado.TelefonoAlternoFiador = FiadorEliminado.TelefonoAlternoFiador;
            objFiadorEliminado.EntregoRecibo_Agua_o_Luz = FiadorEliminado.EntregoRecibo_Agua_o_Luz;
            objFiadorEliminado.NumCartasPersonales = FiadorEliminado.NumCartasPersonales;
            objFiadorEliminado.NumCartasFamiliares = FiadorEliminado.NumCartasFamiliares;

            this._context.FiadorDb.Remove(objFiadorEliminado);
            await this._context.SaveChangesAsync();
        }

    }
}
