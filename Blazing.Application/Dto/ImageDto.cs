using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO Image. 
    /// <summary>
    /// DTO responsible for product images in general.
    /// </summary>
    public sealed class ImageDto : BaseEntityDto
    {
        public string? Url { get; set; }

        /// <summary>
        /// Texto alternativo para a imagem.
        /// </summary>
        public string? AltText { get; set; }
    }
    #endregion
}
