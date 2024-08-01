using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity Address.
    /// <summary>
    /// Entity responsible for the address.
    /// </summary>
    public class Address
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string? Street { get; set; }

        [Required]
        [StringLength(10)]
        public string? Number { get; set; }

        [Required]
        [StringLength(50)]
        public string? Complement { get; set; }

        [Required]
        [StringLength(50)]
        public string? Neighborhood { get; set; }

        [Required]
        [StringLength(50)]
        public string? City { get; set; }

        [Required]
        [StringLength(2)]
        public string? State { get; set; }

        [Required]
        [StringLength(8)]
        public string? PostalCode { get; set; }

        public Guid UserId { get; set; }
    }
    #endregion
}
