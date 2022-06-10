using Migrantes.Models.Entities;
using Migrantes.ViewModels;
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

            ModeloFiador objFiador = new ModeloFiador();

            objFiador.per_codigo_id = FiadorCreado.per_codigo_id;
            objFiador.IdFiador = FiadorCreado.IdFiador;
            objFiador.PrimerNombreFiador = FiadorCreado.PrimerNombreFiador;
            objFiador.SegundoNombreFiador = FiadorCreado.SegundoNombreFiador;
            objFiador.PrimerApellidoFiador = FiadorCreado.PrimerApellidoFiador;
            objFiador.SegundoApellidoFiador = FiadorCreado.SegundoApellidoFiador;
            objFiador.PaisNacimientoFiador = FiadorCreado.PaisNacimientoFiador;
            objFiador.EdadFiador = FiadorCreado.EdadFiador;
            objFiador.SexoFiador = FiadorCreado.SexoFiador;
            objFiador.EmailFiador = FiadorCreado.EmailFiador;
            objFiador.TelefonoFiador = FiadorCreado.TelefonoFiador;
            objFiador.TelefonoAlternoFiador = FiadorCreado.TelefonoAlternoFiador;
            objFiador.EntregoRecibo_Agua_o_Luz = FiadorCreado.EntregoRecibo_Agua_o_Luz;
            objFiador.NumCartasPersonales = FiadorCreado.NumCartasPersonales;
            objFiador.NumCartasFamiliares = FiadorCreado.NumCartasFamiliares;

            this._context.FiadorDb.Add(objFiador);
            await this._context.SaveChangesAsync();
        }


        public async Task EliminarFiadorConfirmado(FiadorViewModel FiadorEliminado)
        {

            ModeloFiador objFiadorEliminado = new ModeloFiador();

            objFiadorEliminado.per_codigo_id = FiadorEliminado.per_codigo_id;
            objFiadorEliminado.IdFiador = FiadorEliminado.IdFiador;
            objFiadorEliminado.PrimerNombreFiador = FiadorEliminado.PrimerNombreFiador;
            objFiadorEliminado.SegundoNombreFiador = FiadorEliminado.SegundoNombreFiador;
            objFiadorEliminado.PrimerApellidoFiador = FiadorEliminado.PrimerApellidoFiador;
            objFiadorEliminado.SegundoApellidoFiador = FiadorEliminado.SegundoApellidoFiador;
            objFiadorEliminado.PaisNacimientoFiador = FiadorEliminado.PaisNacimientoFiador;
            objFiadorEliminado.EdadFiador = FiadorEliminado.EdadFiador;
            objFiadorEliminado.SexoFiador = FiadorEliminado.SexoFiador;
            objFiadorEliminado.EmailFiador = FiadorEliminado.EmailFiador;
            objFiadorEliminado.TelefonoFiador = FiadorEliminado.TelefonoFiador;
            objFiadorEliminado.TelefonoAlternoFiador = FiadorEliminado.TelefonoAlternoFiador;
            objFiadorEliminado.EntregoRecibo_Agua_o_Luz = FiadorEliminado.EntregoRecibo_Agua_o_Luz;
            objFiadorEliminado.NumCartasPersonales = FiadorEliminado.NumCartasPersonales;
            objFiadorEliminado.NumCartasFamiliares = FiadorEliminado.NumCartasFamiliares;

            this._context.FiadorDb.Remove(objFiadorEliminado);
            await this._context.SaveChangesAsync();
        }

        public async Task GuardarFiadorEditado(FiadorViewModel oFiadorEditado)
        {

            ModeloFiador objFiadorEditado = new ModeloFiador();

            objFiadorEditado.per_codigo_id = oFiadorEditado.per_codigo_id;
            objFiadorEditado.IdFiador = oFiadorEditado.IdFiador;
            objFiadorEditado.PrimerNombreFiador = oFiadorEditado.PrimerNombreFiador;
            objFiadorEditado.SegundoNombreFiador = oFiadorEditado.SegundoNombreFiador;
            objFiadorEditado.PrimerApellidoFiador = oFiadorEditado.PrimerApellidoFiador;
            objFiadorEditado.SegundoApellidoFiador = oFiadorEditado.SegundoApellidoFiador;
            objFiadorEditado.PaisNacimientoFiador = oFiadorEditado.PaisNacimientoFiador;
            objFiadorEditado.EdadFiador = oFiadorEditado.EdadFiador;
            objFiadorEditado.SexoFiador = oFiadorEditado.SexoFiador;
            objFiadorEditado.EmailFiador = oFiadorEditado.EmailFiador;
            objFiadorEditado.TelefonoFiador = oFiadorEditado.TelefonoFiador;
            objFiadorEditado.TelefonoAlternoFiador = oFiadorEditado.TelefonoAlternoFiador;
            objFiadorEditado.EntregoRecibo_Agua_o_Luz = oFiadorEditado.EntregoRecibo_Agua_o_Luz;
            objFiadorEditado.NumCartasPersonales = oFiadorEditado.NumCartasPersonales;
            objFiadorEditado.NumCartasFamiliares = oFiadorEditado.NumCartasFamiliares;

            this._context.FiadorDb.Update(objFiadorEditado);
            await this._context.SaveChangesAsync();

        }

    }
}
