using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO Assessment.
    /// <summary>
    /// DTO responsible for the product AssessmentDto.
    /// </summary>
    public class AssessmentDto
    {
        public Guid Id { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "A média deve estar entre 0.0 e 5.0")]
        public double Average { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de avaliações deve ser um valor positivo.")]
        public int NumberOfReviews { get; set; }

        [Required(ErrorMessage = "O ID da revisão é obrigatório.")]
        [ForeignKey("ReviewId")]
        public Guid RevisionId { get; set; }
        public RevisionDto? RevisionDetail { get; set; }
    }
    #endregion
}
