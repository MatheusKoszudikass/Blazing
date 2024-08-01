namespace Blazing.Application.Dto
{
    #region DTO item cart.
    /// <summary>
    /// DTO responsible for the item cart
    /// </summary>
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCartDto? ShoppingCart { get; set; }
    }
    #endregion
}
