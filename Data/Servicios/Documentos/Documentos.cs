using Microsoft.AspNetCore.Hosting;
using Migrantes.Models.DTO;
using Migrantes.Models.Entities;
using Migrantes.ViewModels;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Documentos
{
    public class Documentos : IDocumentos
    {

        private readonly ApplicationDbContext _context;

        public Documentos(ApplicationDbContext context)
        {
            this._context = context;

        }


        public async Task GuardarDocumento(DocumentosPersonaDTO DocGuardado, string fileNameA, string path2, string fileNameB, string path2B)

        {
            IdentidadPersona oDocumento = new IdentidadPersona();

            
            oDocumento.ide_codigo_id = DocGuardado.ide_codigo_id;
            oDocumento.ide_id_documento = DocGuardado.ide_id_documento;
            oDocumento.ide_numero = DocGuardado.ide_numero;
            oDocumento.ide_fecha_emision = DocGuardado.ide_fecha_emision;
            oDocumento.ide_fecha_vencimiento = DocGuardado.ide_fecha_vencimiento;
            oDocumento.ide_estado = 1;
            oDocumento.ide_entregado = true;
            oDocumento.NombreImagenPortada_A = fileNameA;
            oDocumento.RutaImagenPortada_A = path2;

            oDocumento.NombreImagenPortada_B = fileNameB;
            oDocumento.RutaImagenPortada_B = path2B;

            await this._context.IdentidadPersonasDb.AddAsync(oDocumento);
            await this._context.SaveChangesAsync();

        }

        public async Task GuardarDocumentoEditado(IdentidadPersona documentoEditado)
        {

            IdentidadPersona oDocumento = new IdentidadPersona();

            oDocumento.ide_id_persona = documentoEditado.ide_id_persona;
            oDocumento.ide_codigo_id = documentoEditado.ide_codigo_id;
            oDocumento.ide_id_documento = documentoEditado.ide_id_documento;
            oDocumento.ide_numero = documentoEditado.ide_numero;
            oDocumento.ide_fecha_emision = documentoEditado.ide_fecha_emision;
            oDocumento.ide_fecha_vencimiento = documentoEditado.ide_fecha_vencimiento;
            oDocumento.ide_estado = 1;
            oDocumento.ide_entregado = true;
            oDocumento.NombreImagenPortada_A = documentoEditado.NombreImagenPortada_A;
            oDocumento.NombreImagenPortada_B = documentoEditado.NombreImagenPortada_B;
            oDocumento.RutaImagenPortada_A = documentoEditado.RutaImagenPortada_A;
            oDocumento.RutaImagenPortada_B = documentoEditado.RutaImagenPortada_B;

            this._context.IdentidadPersonasDb.Update(oDocumento);
            await this._context.SaveChangesAsync();

        }

        public async Task EliminarDocumentoConfirmado(DocumentosViewModel documentoEliminado)

        {

            IdentidadPersona oDocumento = new IdentidadPersona();

            oDocumento.ide_id_persona = documentoEliminado.ide_id_persona;
            oDocumento.ide_codigo_id = documentoEliminado.ide_codigo_id;
            oDocumento.ide_id_documento = documentoEliminado.ide_id_documento;
            oDocumento.ide_numero = documentoEliminado.ide_numero;
            oDocumento.ide_fecha_emision = documentoEliminado.ide_fecha_emision;
            oDocumento.ide_fecha_vencimiento = documentoEliminado.ide_fecha_vencimiento;
            oDocumento.ide_estado = 1;
            oDocumento.ide_entregado = true;

            this._context.IdentidadPersonasDb.Remove(oDocumento);
            await this._context.SaveChangesAsync();

        }

    }
}



