
using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blazing.Application.Dto
{
    #region Entity User.
    /// <summary>
    /// Entity responsible for the user.
    /// </summary>
    public sealed class UserDto : BaseEntityDto
    {
        public Guid IdentityId { get; set; }
        public bool Status { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? PhoneNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<AddressDto>? Addresses { get; set; } = new List<AddressDto>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<ShoppingCartDto>? ShoppingCarts { get; set; } = new List<ShoppingCartDto>();
    }
    #endregion
}
