namespace Blazing.Application.Dto
{
    #region DTO Add cart.
    /// <summary>
    /// DTO responsible for adding items to the shopping cart.
    /// </summary>
    public class AddCartItemDto
    {
        public Guid Id { get; set; } 

        public Guid ProductId { get; set; }

        public ProductDto? Product { get; set; }

        public int Quantity { get; set; }
    }
    #endregion
}

