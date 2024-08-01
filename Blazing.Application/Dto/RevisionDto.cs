namespace Blazing.Application.Dto
{
    #region DTO revision.
    /// <summary>
    /// DTO responsible for the product review made by the user.
    /// </summary>
    public class RevisionDto
    {
        public Guid Id { get; set; }
        public string? User { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
    }
    #endregion
}
