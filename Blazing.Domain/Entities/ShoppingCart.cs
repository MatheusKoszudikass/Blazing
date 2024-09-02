using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity shopping cart.
    /// <summary>
    /// Entity responsible for the shopping cart creation linked with the logged-in user in the system.
    /// </summary>
    public sealed class ShoppingCart : BaseEntity
    {
        [ForeignKey("User")]
        [Required(ErrorMessage = "O identificador do usuário é obrigatório.")]
        public Guid UserId { get; set; }

        public User? User { get; set; } 

        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        public IEnumerable<CartItem?> Items { get; set; } = [];

        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalValue { get; set; }
    }
    #endregion
}
