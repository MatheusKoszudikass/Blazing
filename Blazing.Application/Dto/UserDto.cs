namespace Blazing.Application.Dto
{
    #region DTO User.
    /// <summary>
    /// DTO responsible for the user.
    /// </summary>
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime LastUpdate { get; set; }

        public IEnumerable<AddressDto>? Addresses { get; set; } = [];

        public IEnumerable<ShoppingCartDto>? ShoppingCarts { get; set; } = [];
    }
    #endregion
}
