using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Application.Dto
{
    #region DTO item cart.
    /// <summary>
    /// DTO responsible for the item cart
    /// </summary>
    public sealed class CartItemDto : BaseEntityDto
    {
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }
    }
    #endregion
}
