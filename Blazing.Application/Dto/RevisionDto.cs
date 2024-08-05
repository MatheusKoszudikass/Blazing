using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO revision.
    /// <summary>
    /// DTO responsible for the product review made by the user.
    /// </summary>
    public class RevisionDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do usuário não pode ter mais de 100 caracteres.")]
        public string? User { get; set; }

        [Required(ErrorMessage = "O comentário é obrigatório.")]
        [StringLength(1000, ErrorMessage = "O comentário não pode ter mais de 1000 caracteres.")]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "A data da revisão é obrigatória.")]
        public DateTime Date { get; set; }
    } 
    #endregion
}
