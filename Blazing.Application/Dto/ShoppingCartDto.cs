using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO shopping cart.
    /// <summary>
    /// DTO responsible for the shopping cart creation linked with the logged-in user in the system.
    /// </summary>
    public sealed class ShoppingCartDto : BaseEntityDto
    {
        [Required(ErrorMessage = "O identificador do usuário é obrigatório.")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Details of the user associated with the shopping cart.
        /// </summary>
        public UserDto? User { get; set; }

        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        public IEnumerable<CartItemDto?> Items { get; set; } = [];

        [Range(0, double.MaxValue, ErrorMessage = "O valor total deve ser um valor positivo.")]
        public decimal TotalValue { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
    #endregion
}
