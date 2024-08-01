using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity Availability.
    /// <summary>
    /// Entity responsible for providing information on whether the product is available.
    /// </summary>
    public class Availability
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsAvailable { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
    #endregion
}
