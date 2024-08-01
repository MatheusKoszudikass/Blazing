namespace Blazing.Application.Dto
{
    #region DTO Availability.
    /// <summary>
    /// DTO responsible for providing information on whether the product is available.
    /// </summary>
    public class AvailabilityDto
    {
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
    #endregion
}
