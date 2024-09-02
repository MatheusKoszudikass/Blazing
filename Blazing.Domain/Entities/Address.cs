using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Domain.Entities
{
    #region Entity Address.
    /// <summary>
    /// Entity responsible for the address.
    /// </summary>
    public sealed class Address : BaseEntity
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Rua é obrigatório.")]
        [StringLength(100, ErrorMessage = "Rua deve ter entre 3 e 100 caracteres.", MinimumLength = 3)]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Número é obrigatório.")]
        [StringLength(10, ErrorMessage = "Número deve ter entre 1 e 10 caracteres.", MinimumLength = 1)]
        public string? Number { get; set; }

        [Required(ErrorMessage = "Complemento é obrigatório.")]
        [StringLength(100, ErrorMessage = "Complemento deve ter entre 3 e 100 caracteres.", MinimumLength = 3)]
        public string? Complement { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório.")]
        [StringLength(50, ErrorMessage = "Bairro deve ter entre 3 e 50 caracteres.", MinimumLength = 3)]
        public string? Neighborhood { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatório.")]
        [StringLength(50, ErrorMessage = "Cidade deve ter entre 3 e 50 caracteres.", MinimumLength = 3)]
        public string? City { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório.")]
        [StringLength(2, ErrorMessage = "Estado deve ter 2 caracteres.", MinimumLength = 2)]
        public string? State { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório.")]
        [StringLength(8, ErrorMessage = "CEP deve ter 8 caracteres.", MinimumLength = 8)]
        public string? PostalCode { get; set; }
    }
    #endregion
}