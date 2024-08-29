using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Blazing.Application.Dto
{
    #region DTO category.
    /// <summary>
    /// DTO responsible for grouping products by categories.
    /// </summary>
    public sealed class CategoryDto : BaseEntityDto
    {
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<ProductDto?> Products { get; set; } = new List<ProductDto?>();
    }
    #endregion
}
