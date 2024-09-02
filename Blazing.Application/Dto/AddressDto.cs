using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Application.Dto
{
 #region DTO Address.
    /// <summary>
    /// DTO responsible for the address.
    /// </summary>
    public sealed class AddressDto : BaseEntityDto
    {
        public Guid UserId { get; set; }

        public string? Street { get; set; }

        public string? Number { get; set; }

        public string? Complement { get; set; }

        public string? Neighborhood { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }
    }
#endregion
}
