
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
        [StringLength(50, ErrorMessage = "A cor não pode ter mais de 50 caracteres")]
        public string? Color { get; set; }

        [StringLength(100, ErrorMessage = "O material não pode ter mais de 100 caracteres")]
        public string? Material { get; set; }

        [StringLength(100, ErrorMessage = "O modelo não pode ter mais de 100 caracteres")]
        public string? Model { get; set; }
    }
    #endregion
}
