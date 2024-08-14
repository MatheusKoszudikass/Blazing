using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity Image. 
    /// <summary>
    /// Entity responsible for product images in general.
    /// </summary>
    public sealed class Image : BaseEntity
    {
        public string? Url { get; set; }

        /// <summary>
        /// Texto alternativo para a imagem.
        /// </summary>
        public string? AltText { get; set; }
    }
    #endregion
}
