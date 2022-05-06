using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Migrantes.Models.Entities;


namespace Migrantes.Data.Configuraciones
{
    public class EstadoCivilConfig : IEntityTypeConfiguration<EstadoCivil>
    {
        public void Configure(EntityTypeBuilder<EstadoCivil> builder)
        {

            builder.ToTable("estado_civil", "mig");
        }
    }
}
