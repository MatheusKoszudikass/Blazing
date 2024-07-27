namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável por adicionar item.
    /// </summary>
    public class CarrinhoItemAdd
    {
        public Guid CarrinhoId { get; set; } = new Guid();

        public Guid ProdutoId { get; set; }

        public  Produto? Produtos { get; set; }

        public int Quantidade { get; set; }
    }
}
