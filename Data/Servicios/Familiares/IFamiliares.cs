using Migrantes.ViewModels;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Familiares
{
    public interface IFamiliares
    {
        Task ActualizarFamiliaresEditados(DatosFamiliaresViewModel DatosFamiliaresEditados);
        Task AgregandoDatosFamiliares(DatosFamiliaresViewModel oDatosFamiliares);
        Task EliminarConfirmadoDatosFamiliares(DatosFamiliaresViewModel DatosFamiliaresEliminados);
    }
}
