using Migrantes.ViewModels;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Fiador
{
    public interface IFiador
    {
        Task GuardarFiador(FiadorViewModel FiadorCreado);
    }
}
