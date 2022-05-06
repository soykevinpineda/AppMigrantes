using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Migrantes.Models.Entities;


namespace Migrantes.Data.Configuraciones
{
    public class DatosFamiliaresConfig : IEntityTypeConfiguration<DatosFamiliares>
    {
        public void Configure(EntityTypeBuilder<DatosFamiliares> builder)
        {
            builder.ToTable("datos_familiares", "mig");
        }
    }
}
