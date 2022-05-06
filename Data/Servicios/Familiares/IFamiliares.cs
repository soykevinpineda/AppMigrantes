using Migrantes.ViewModels;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Familiares
{
    public interface IFamiliares
    {
        Task AgregandoDatosFamiliares(DatosFamiliaresViewModel oDatosFamiliares);
    }
}
