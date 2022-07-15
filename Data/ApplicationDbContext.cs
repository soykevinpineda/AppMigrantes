using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Migrantes.Data.Configuraciones;
using Migrantes.Models.Entities;
using System.Reflection;

namespace Migrantes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        //Aqui se agregan los modelos que se usaran
        //para usarlos en ApplicationDbContext.
        public virtual DbSet<Persona> PersonasDb { get; set; }
        public virtual DbSet<IdentidadPersona> IdentidadPersonasDb { get; set; }
        public virtual DbSet<DatosFamiliares> DatosFamiliaresDb { get; set; }
        public virtual DbSet<ModeloFiador> FiadorDb { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumentosDb { get; set; }
        public virtual DbSet<TipoEstado> TipoEstadosDb { get; set; }
        public virtual DbSet<Sexo> SexosDb { get; set; }
        public virtual DbSet<EstadoCivil> EstadoCivilDb { get; set; }

        public virtual DbSet<Parientes>ParientesDb { get; set; }



        #region ConnectionString
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionStrings.MigrantesDb);
            }
        }
        #endregion

        #region ConfiguracionesDbSets
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //builder.ApplyConfiguration(new PersonaConfig());

            base.OnModelCreating(builder);
        }
        #endregion
    }
}
