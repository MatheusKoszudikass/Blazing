using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO Add cart.
    /// <summary>
    /// DTO responsible for adding items to the shopping cart.
    /// </summary>
    public sealed class AddCartItemDto : BaseEntityDto
    {
        public Guid ProductId { get; set; }

        public ProductDto? Product { get; set; }

        public int Quantity { get; set; }
    }
    #endregion
}

