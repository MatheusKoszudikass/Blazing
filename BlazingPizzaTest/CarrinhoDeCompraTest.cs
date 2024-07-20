using BlazingPizza.Api.Entites;
using BlazingPizza.Api.Repositories.Services;

namespace BlazingPizzaTest
{
    public class CarrinhoDeCompraTest
    {
        [Fact]
        public void CriarItemDb()
        {
            var CarrinhoItemID = 1;
            var ProdutoId = 1;
            var Produto = new List<Produto>();
            var Quantidade = 10000000000000;
            var CarrinhoDeCompraId = 1;
            var CarrinhoDeCompra = new CarrinhoDeCompra();


            var CarrinhoItemAdd = new CarrinhoItemAdd();

            var CarrinhoDeItensRepository = new CarrinhoCompraRepository();

            CarrinhoDeItensRepository.AddItem(CarrinhoItemAdd);
        }
    }
}