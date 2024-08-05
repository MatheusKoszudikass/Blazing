using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity item cart.
    /// <summary>
    /// Entity responsible for the item cart
    /// </summary>
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "O ID do carrinho de compras é obrigatório.")]
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }
    #endregion
}
