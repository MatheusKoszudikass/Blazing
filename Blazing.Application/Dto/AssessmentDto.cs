using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Blazing.Domain.Entities;
using System.Text.Json.Serialization;

namespace Blazing.Application.Dto
{
    #region DTO Assessment.
    /// <summary>
    /// DTO responsible for the product AssessmentDto.
    /// </summary>
    public sealed class AssessmentDto : BaseEntityDto
    {
        [Range(0.0, 5.0, ErrorMessage = "A média deve estar entre 0.0 e 5.0")]
        public double Average { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de avaliações deve ser um valor positivo.")]
        public int NumberOfReviews { get; set; }

        [Required(ErrorMessage = "O ID da revisão é obrigatório.")]
        [ForeignKey("ReviewId")]
        public Guid RevisionId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<RevisionDto?> RevisionDetail { get; set; } = new List<RevisionDto?>();
    }
    #endregion
}
