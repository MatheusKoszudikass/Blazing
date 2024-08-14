using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Domain.Entities
{
    #region Entity Assessment
    /// <summary>
    /// Entity responsible for the product review.
    /// </summary>
    public sealed class Assessment : BaseEntity
    {
        [Range(0.0, 5.0, ErrorMessage = "A média deve estar entre 0.0 e 5.0")]
        public double Average { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de avaliações deve ser um valor positivo.")]
        public int NumberOfReviews { get; set; }

        [Required(ErrorMessage = "O ID da revisão é obrigatório.")]

        [ForeignKey("RevisionId")]
        public Guid RevisionId { get; set; }

        public IEnumerable<Revision> RevisionDetail { get; set; } = [];
    }
    #endregion
}
