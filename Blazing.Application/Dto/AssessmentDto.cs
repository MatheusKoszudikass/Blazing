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
        public double Average { get; set; }

        public int NumberOfReviews { get; set; }

        public Guid RevisionId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<RevisionDto?> RevisionDetail { get; set; } = new List<RevisionDto?>();
    }
    #endregion
}
