using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity User.
    /// <summary>
    /// Entity responsible for the user.
    /// </summary>
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string? Username { get; set; }

        [Required]
        [StringLength(255)]
        public string? Email { get; set; }

        [Required]
        [StringLength(128)]
        public string? PasswordHash { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime LastUpdate { get; set; }

        public IEnumerable<Address>? Addresses { get; set; } = [];

        public IEnumerable<ShoppingCart>? ShoppingCarts { get; set; } = [];
    }
    #endregion
}
