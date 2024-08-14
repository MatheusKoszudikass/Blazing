using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Blazing.Domain.Entities
{
    #region Entity Add cart.
    /// <summary>
    /// Entity responsible for adding items to the shopping cart.
    /// </summary>
    public sealed class AddCartItem : BaseEntity
    {
        [Required]
        public Guid ProductId { get; set; }

        public Product? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantidade tem que ser maior que zero.")]
        public int Quantity { get; set; }
    }
    #endregion
}
