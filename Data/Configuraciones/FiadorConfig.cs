using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Migrantes.Models.Entities;

namespace Migrantes.Data.Configuraciones
{
    public class FiadorConfig : IEntityTypeConfiguration<ModeloFiador>
    {
        public void Configure(EntityTypeBuilder<ModeloFiador> builder)
        {
            builder.ToTable("fiador", "mig");
        }
    }

}
