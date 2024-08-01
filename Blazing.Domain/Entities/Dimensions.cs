using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity Dimensions.
    /// <summary>
    /// Entity responsible for the product dimensions.
    /// </summary>
    public class Dimensions
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public string Unit { get; set; } = "cm";
    }
    #endregion
}
