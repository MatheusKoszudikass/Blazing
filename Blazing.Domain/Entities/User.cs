using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Domain.Entities
{
    #region Entity User.
    /// <summary>
    /// Entity responsible for the user.
    /// </summary>
    public sealed class User : BaseEntity
    {
        public Guid IdentityId { get; set; }

        [Required(ErrorMessage = "Obrigatório informa se o usuário e ativo ou inativo.")]
        public bool? Status { get; set; }

        [Required(ErrorMessage = "O primeiro nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O primeiro nome não pode ter mais de 50 caracteres.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O sobrenome não pode ter mais de 50 caracteres.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(30, ErrorMessage = "O nome de usuário não pode ter mais de 30 caracteres.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser um endereço de e-mail válido.")]
        [StringLength(100, ErrorMessage = "O e-mail não pode ter mais de 100 caracteres.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O hash da senha é obrigatório.")]
        public string? PasswordHash { get; set; }

        public string? PhoneNumber { get; set; }

        public IEnumerable<Address>? Addresses { get; set; } = new List<Address>();
        public IEnumerable<ShoppingCart>? ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
    #endregion
}
