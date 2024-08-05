using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Domain.Entities
{
    #region Entity Assessment
    /// <summary>
    /// Entity responsible for the product review.
    /// </summary>
    public class Assessment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Range(0.0, 5.0, ErrorMessage = "A média deve estar entre 0.0 e 5.0")]
        public double Average { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de avaliações deve ser um valor positivo.")]
        public int NumberOfReviews { get; set; }

        [Required(ErrorMessage = "O ID da revisão é obrigatório.")]
        [ForeignKey("ReviewId")]
        public Guid RevisionId { get; set; }

        public Revision? RevisionDetail { get; set; }
    }
    #endregion
}
