using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace Blazing.Application.Dto
{
    #region DTO shopping cart.
    /// <summary>
    /// DTO responsible for the shopping cart creation linked with the logged-in user in the system.
    /// </summary>
    public sealed class ShoppingCartDto : BaseEntityDto
    {
        public Guid UserId { get; set; }

        public IEnumerable<CartItemDto?> Items { get; set; } = [];

        public decimal TotalValue { get; set; }

    }
    #endregion
}
