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
        public double Average { get; set; }
        public int NumberOfReviews { get; set; }

        [ForeignKey("ReviewId")]
        public Guid RevisionId { get; set; }

        public Revision? RevisionDetail { get; set; }
    }
    #endregion
}
