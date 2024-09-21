
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
public record UserDto
{
    public Guid Id { get; init; }
    public bool Status { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; }= string.Empty;
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string? PasswordHash { get; init; }
    public string? PhoneNumber { get; init; }
    public DateTime DateCreate { get; init; }
    public DateTime? DateUpdate { get; init; }
    public DateTime? DateDelete { get; init; }
    public ICollection<Address>? Addresses { get; init; } = new List<Address>();
    public ICollection<Role> Roles { get; init; } = new List<Role>();
    public ICollection<ShoppingCart>? ShoppingCarts { get; init; } = new List<ShoppingCart>();

        // Construtor sem parâmetros
        public UserDto() {}
}

    #endregion
}
