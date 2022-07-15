using Migrantes.Models.DTO;
using Migrantes.ViewModels;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Familiares
{
    public interface IFamiliares
    {
        Task AgregandoDatosFamiliares(DatosFamiliaresDTO DatosFamiliaresAgregados);

        Task EliminarConfirmadoDatosFamiliares(DatosFamiliaresDTO DatosFamiliaresEliminados);

        Task ActualizarFamiliaresEditados(DatosFamiliaresDTO DatosFamiliaresEditados);

    }

}
