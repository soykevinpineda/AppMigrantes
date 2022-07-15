using Migrantes.Models.DTO;
using Migrantes.Models.Entities;
using Migrantes.ViewModels;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Documentos
{
    public interface IDocumentos
    {
        Task EliminarDocumentoConfirmado(DocumentosViewModel IdentidadEliminada);
        Task GuardarDocumento(DocumentosPersonaDTO DocGuardado, string path2, string fileName, string path2B, string fileNameB);
        Task GuardarDocumentoEditado(IdentidadPersona identidad);
    }
}
