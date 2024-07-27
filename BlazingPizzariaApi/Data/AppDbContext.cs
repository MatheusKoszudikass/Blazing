using BlazingPizza.Api.Entites;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Api.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {


        }

        public DbSet<Atributo> Atributo { get; set; }
        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<CarrinhoDeCompra> CarrinhoDeCompra { get; set; }
        public DbSet<CarrinhoDeItem> CarrinhoDeItem { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Dimensoes> Dimensoes { get; set; }
        public DbSet<Disponibilidade> Disponibilidade { get; set; }
        public DbSet<Imagem> Imagem { get; set; }
        public DbSet<Produto> Produto  { get; set; }
        public DbSet<Revisao> Revisao { get; set; }    
        public DbSet<Usuario> Usuario { get; set; }

    }
}
