using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Blazing.Domain.Entities
{
    #region Entity Add cart.
    /// <summary>
    /// Entity responsible for adding items to the shopping cart.
    /// </summary>
    public class AddCartItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }
    }
    #endregion
}
