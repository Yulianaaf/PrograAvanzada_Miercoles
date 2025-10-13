using Microsoft.EntityFrameworkCore;

namespace MyPymeStore.Models
{
    public class MyPymeStoreContext : DbContext
    {
        public MyPymeStoreContext(DbContextOptions<MyPymeStoreContext> options)
            : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Categoria>().HasKey(c => c.Id);
            modelBuilder.Entity<Producto>().HasKey(p => p.Id);

            
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
