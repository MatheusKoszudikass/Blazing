using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity shopping cart.
    /// <summary>
    /// Entity responsible for the shopping cart creation linked with the logged-in user in the system.
    /// </summary>
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        public IEnumerable<CartItem?> Items { get; set; } = [];

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalValue { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
    #endregion
}
