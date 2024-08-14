﻿using Blazing.Domain.Entities;
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

        [Range(1, int.MaxValue, ErrorMessage = "Quantidade tem que ser maior que zero.")]
        public int Quantity { get; set; }
    }
    #endregion
}

