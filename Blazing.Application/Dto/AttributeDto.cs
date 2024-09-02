
using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO Attribute.
    /// <summary>
    /// DTO responsible for the product attributes.
    /// </summary>
    public sealed class AttributeDto : BaseEntityDto
    {
        public string? Color { get; set; }

        public string? Material { get; set; }

        public string? Model { get; set; }
    }
    #endregion
}
