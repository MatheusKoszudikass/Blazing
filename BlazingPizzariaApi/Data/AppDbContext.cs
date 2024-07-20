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

        public DbSet<Atributos> Atributos { get; set; }
        public DbSet<Avaliacao> Avaliacaos { get; set; }
        public DbSet<CarrinhoDeCompra> CarrinhoDeCompra { get; set; }
        public DbSet<CarrinhoDeItems> CarrinhoDeItems { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Dimensoes> Dimensoes { get; set; }
        public DbSet<Disponibilidade> Disponibilidades { get; set; }
        public DbSet<Imagem> Imagem { get; set; }
        public DbSet<Produto> Produtos  { get; set; }
        public DbSet<Revisao> Revisaos { get; set; }    
        public DbSet<Usuario> Usuarios  { get; set; }

    }
}
