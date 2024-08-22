using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Blazing.Application.Dto
{
    #region DTO shopping cart.
    /// <summary>
    /// DTO responsible for the shopping cart creation linked with the logged-in user in the system.
    /// </summary>
    public sealed class ShoppingCartDto : BaseEntityDto
    {
        /// <summary>
        /// Details of the user associated with the shopping cart.
        /// </summary>
        public ApplicationUser? User { get; set; }

        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        public IEnumerable<CartItemDto?> Items { get; set; } = [];

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor total deve ser um valor positivo.")]
        public decimal TotalValue { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
    #endregion
}
