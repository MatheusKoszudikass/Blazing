namespace Blazing.Application.Dto
{
    #region DTO Image. 
    /// <summary>
    /// DTO responsible for product images in general.
    /// </summary>
    public class ImageDto
    {
        public Guid Id { get; set; }
        public string? Url { get; set; }
        public string? AltText { get; set; }
    }
    #endregion
}
