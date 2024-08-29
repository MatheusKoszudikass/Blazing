using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO Availability.
    /// <summary>
    /// DTO responsible for providing information on whether the product is available.
    /// </summary>
    public sealed class AvailabilityDto : BaseEntityDto
    {
        public bool IsAvailable { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }
    }
    #endregion
}
