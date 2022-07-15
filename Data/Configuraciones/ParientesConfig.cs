using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Migrantes.Models.Entities;

namespace Migrantes.Data.Configuraciones
{
    public class ParientesConfig : IEntityTypeConfiguration<Parientes>
    {
        public void Configure(EntityTypeBuilder<Parientes> builder)
        {
            builder.ToTable("parientes", "mig");
        }
    }
}
