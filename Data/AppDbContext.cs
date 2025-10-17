using Microsoft.EntityFrameworkCore;

namespace Usuario.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ⚠️ Verifique o endereço, porta e service name do seu Oracle
            optionsBuilder.UseOracle("Data Source=//oracle.fiap.com.br:1521/orcl;User Id=rm560914;Password=fiap25");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define o nome exato da tabela no Oracle
            modelBuilder.Entity<Models.Usuario>().ToTable("USUARIOX");
        }
    }
}