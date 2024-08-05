using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO Image. 
    /// <summary>
    /// DTO responsible for product images in general.
    /// </summary>
    public class ImageDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A URL da imagem é obrigatória.")]
        [Url(ErrorMessage = "A URL fornecida não é válida.")]
        public string? Url { get; set; }

        /// <summary>
        /// Texto alternativo para a imagem.
        /// </summary>
        [StringLength(200, ErrorMessage = "O texto alternativo não pode ter mais de 200 caracteres.")]
        public string? AltText { get; set; }
    }
    #endregion
}
