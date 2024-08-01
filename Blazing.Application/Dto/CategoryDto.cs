namespace Blazing.Application.Dto
{
    #region DTO category.
    /// <summary>
    /// DTO responsible for grouping products by categories.
    /// </summary>
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductDto?> Products { get; set; } = [];
    }
    #endregion
}
