using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Migrantes.Models.Entities;

namespace Migrantes.Data.Configuraciones
{
    public class IdentidadPersonaConfig : IEntityTypeConfiguration<IdentidadPersona>
    {
        public void Configure(EntityTypeBuilder<IdentidadPersona> builder)
        {
            builder.ToTable("ide_identidad", "mig");
            builder.Property(x => x.NombreImagenPortada_A).HasColumnName("NombreImagenPortada_A");
            builder.Property(x => x.RutaImagenPortada_A).HasColumnName("RutaImagenPortada_A");
        }
    }
}
