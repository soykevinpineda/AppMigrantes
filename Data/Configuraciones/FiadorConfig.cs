using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Migrantes.Models.Entities;

namespace Migrantes.Data.Configuraciones
{
    public class FiadorConfig : IEntityTypeConfiguration<Fiador>
    {
        public void Configure(EntityTypeBuilder<Fiador> builder)
        {
            builder.ToTable("fiador", "mig");
        }
    }

}
