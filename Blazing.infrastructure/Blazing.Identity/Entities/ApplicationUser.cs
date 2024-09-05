
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Identity.Entities
{
    #region DTO User.
    /// <summary>
    /// DTO responsible for the user.
    /// </summary>
    public sealed class ApplicationUser : IdentityUser<Guid>
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "O primeiro nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O primeiro nome não pode ter mais de 50 caracteres.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O sobrenome não pode ter mais de 50 caracteres.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "A data da última atualização é obrigatória.")]
        public DateTime LastUpdate { get; set; }
    }
    #endregion
}
