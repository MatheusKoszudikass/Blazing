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

        [Required(ErrorMessage = "Rua é obrigatória.")]
        [StringLength(100, ErrorMessage = "Rua deve ter entre 3 e 100 caracteres.", MinimumLength = 3)]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Número é obrigatorio.")]
        [StringLength(10, ErrorMessage = "Número deve ter entre 1 e 10 caracteres.", MinimumLength = 1)]
        public string? Number { get; set; }

        [Required(ErrorMessage = "Complemento é obrigatorio.")]
        [StringLength(100, ErrorMessage = "Complemento deve ter entre 3 e 100 caracteres.", MinimumLength = 3)]
        public string? Complement { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatorio.")]
        [StringLength(50, ErrorMessage = "Bairro deve ter entre 3 e 50 caracteres.", MinimumLength = 3)]
        public string? Neighborhood { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatorio.")]
        [StringLength(50, ErrorMessage = "Cidade deve ter entre 3 e 50 caracteres.", MinimumLength = 3)]
        public string? City { get; set; }

        [Required(ErrorMessage = "Estado é obrigatorio.")]
        [StringLength(2, ErrorMessage = "Estado deve ter 2 caracteres.", MinimumLength = 2)]
        public string? State { get; set; }

        [Required(ErrorMessage = "CEP é obrigatorio.")]
        [StringLength(8, ErrorMessage = "CEP deve ter 8 caracteres.", MinimumLength = 8)]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Usuario é obrigatorio.")]
        public Guid UserId { get; set; }
    }
    #endregion
}