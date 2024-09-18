using Microsoft.EntityFrameworkCore;
using SistemaDeRegistros.Data.Map;

namespace SistemaDeRegistros.Data
{
    public class SistemaTarefasDBContext : DbContext
    {
        public SistemaTarefasDBContext(DbContextOptions<SistemaTarefasDBContext> options)
            : base(options)
        {
        }

        public DbSet<Models.UserModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            //Nota
            base.OnModelCreating(modelBuilder);
        }
    }
}
