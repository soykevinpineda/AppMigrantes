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
    }
}
