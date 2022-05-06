using Migrantes.Models.DTO;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Personas
{
    public interface IPersonas
    {
       Task GuardarPersona(PersonaDTO oPersona);
    }
}