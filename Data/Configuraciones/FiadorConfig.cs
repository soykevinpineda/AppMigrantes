using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Migrantes.Models.Entities;

namespace Migrantes.Data.Configuraciones
{
    public class FiadorConfig : IEntityTypeConfiguration<ClaseFiador>
    {
        public void Configure(EntityTypeBuilder<ClaseFiador> builder)
        {
            builder.ToTable("fiador", "mig");
        }
    }

}
