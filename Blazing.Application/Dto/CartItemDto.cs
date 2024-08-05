using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO item cart.
    /// <summary>
    /// DTO responsible for the item cart
    /// </summary>
    public class CartItemDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "O ID do carrinho de compras é obrigatório.")]
        public Guid ShoppingCartId { get; set; }
        public ShoppingCartDto? ShoppingCart { get; set; }
    }
    #endregion
}
