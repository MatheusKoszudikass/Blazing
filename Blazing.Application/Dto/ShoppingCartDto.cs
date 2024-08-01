namespace Blazing.Application.Dto
{
    #region DTO shopping cart.
    /// <summary>
    /// DTO responsible for the shopping cart creation linked with the logged-in user in the system.
    /// </summary>
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDto? User { get; set; }
        public IEnumerable<CartItemDto?> Items { get; set; } = [];
        public decimal TotalValue { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
    #endregion
}
