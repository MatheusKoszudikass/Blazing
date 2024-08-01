namespace Blazing.Application.Dto
{
    #region DTO Assessment.
    /// <summary>
    /// DTO responsible for the product AssessmentDto.
    /// </summary>
    public class AssessmentDto
    {
        public Guid Id { get; set; }
        public double Average { get; set; }
        public int NumberOfReviews { get; set; }
        public Guid RevisionId { get; set; }
        public RevisionDto? RevisionDetail { get; set; }
    }
    #endregion
}
