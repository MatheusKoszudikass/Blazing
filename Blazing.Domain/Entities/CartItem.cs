using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Domain.Entities
{
    #region Entity item cart.
    /// <summary>
    /// Entity responsible for the item cart
    /// </summary>
    public sealed class CartItem : BaseEntity
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }
    }
    #endregion
}
