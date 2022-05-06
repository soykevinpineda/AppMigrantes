using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Migrantes.Models.Entities;

namespace Migrantes.Data.Configuraciones
{
    public class TipoEstadoConfig : IEntityTypeConfiguration<TipoEstado>
    {
        public void Configure(EntityTypeBuilder<TipoEstado> builder)
        {
            builder.ToTable("tep_tipo_estado", "mig");
        }
    }
}
