using Microsoft.AspNetCore.Hosting;
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


        public async Task GuardarDocumentoEditado(DocumentosViewModel identidad)
        {

            IdentidadPersona oDocumento = new IdentidadPersona();

            oDocumento.ide_codigo_id = identidad.ide_codigo_id;
            oDocumento.ide_id_documento = identidad.ide_id_documento;
            oDocumento.ide_numero = identidad.ide_numero;
            oDocumento.ide_fecha_emision = identidad.ide_fecha_emision;
            oDocumento.ide_fecha_vencimiento = identidad.ide_fecha_vencimiento;
            oDocumento.ide_estado = 1;
            oDocumento.ide_entregado = true;
            oDocumento.NombreImagenPortada_A = identidad.NombreImagenPortada_A;
            oDocumento.NombreImagenPortada_B = identidad.NombreImagenPortada_B;
            oDocumento.RutaImagenPortada_A = identidad.RutaImagenPortada_A;
            oDocumento.RutaImagenPortada_B = identidad.RutaImagenPortada_B;
  
            this._context.IdentidadPersonasDb.Update(oDocumento);
            await this._context.SaveChangesAsync();

        }


        public async Task GuardarDocumento(IdentidadPersona DocGuardado, string fileName, string path2, string fileNameB,string path2B)

        {
            IdentidadPersona oDocumento = new IdentidadPersona();

            oDocumento.ide_codigo_id = DocGuardado.ide_codigo_id;
            oDocumento.ide_id_documento = DocGuardado.ide_id_documento;
            oDocumento.ide_numero = DocGuardado.ide_numero;
            oDocumento.ide_fecha_emision = DocGuardado.ide_fecha_emision;
            oDocumento.ide_fecha_vencimiento = DocGuardado.ide_fecha_vencimiento;
            oDocumento.ide_estado = 1;
            oDocumento.ide_entregado = true;
            oDocumento.NombreImagenPortada_A = fileName;
            oDocumento.RutaImagenPortada_A = path2;

            oDocumento.NombreImagenPortada_B = fileNameB;
            oDocumento.RutaImagenPortada_B = path2B;

            await this._context.IdentidadPersonasDb.AddAsync(oDocumento);
            await this._context.SaveChangesAsync();

        }


        public async Task EliminarDocumento(DocumentosViewModel IdentidadEliminada)

        {

            IdentidadPersona oDocumento = new IdentidadPersona();

            oDocumento.ide_id_persona = IdentidadEliminada.ide_id_persona;
            oDocumento.ide_codigo_id = IdentidadEliminada.ide_codigo_id;
            oDocumento.ide_id_documento = IdentidadEliminada.ide_id_documento;
            oDocumento.ide_numero = IdentidadEliminada.ide_numero;
            oDocumento.ide_fecha_emision = IdentidadEliminada.ide_fecha_emision;
            oDocumento.ide_fecha_vencimiento = IdentidadEliminada.ide_fecha_vencimiento;
            oDocumento.ide_estado = 1;
            oDocumento.ide_entregado = true;

            this._context.IdentidadPersonasDb.Remove(oDocumento);
            await this._context.SaveChangesAsync();

        }

    }
}



