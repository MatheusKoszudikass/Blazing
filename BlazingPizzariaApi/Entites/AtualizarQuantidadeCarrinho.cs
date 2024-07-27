namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável por atualizar a quantidade do carrinho de compra 
    /// </summary>
    public class AtualizarQuantidadeCarrinho
    {
        public Guid CarrinhoDeItemId { get; set; } = new Guid();


        public int Quantidade { get; set; }
    }
}
