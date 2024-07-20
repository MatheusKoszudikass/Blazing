namespace BlazingPizza.Api.Entites
{
    public class CarrinhoItemAdd
    {
        public int CarrinhoId { get; set; }

        public int ProdutoId { get; set; }

        public  Produto? Produtos { get; set; }

        public int Quantidade { get; set; }
    }
}
