using AspMvcNet.Models;
using Microsoft.EntityFrameworkCore;

namespace AspMvcNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoFoto> ProductosFotos { get; set; }
    }
}
