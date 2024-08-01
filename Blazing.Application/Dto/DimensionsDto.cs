namespace Blazing.Application.Dto
{
    #region DTO Dimensions.
    /// <summary>
    /// DTO responsible for the product dimensions.
    /// </summary>
    public class DimensionsDto
    {
        public Guid Id { get; set; } 
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public string Unit { get; set; } = "cm";
    }
    #endregion
}
