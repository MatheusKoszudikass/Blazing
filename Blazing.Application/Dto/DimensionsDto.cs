using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO Dimensions.
    /// <summary>
    /// DTO responsible for the product dimensions.
    /// </summary>
    public sealed class DimensionsDto : BaseEntityDto
    {
        [Range(0.0, double.MaxValue, ErrorMessage = "O peso deve ser um valor positivo.")]
        public double Weight { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "A altura deve ser um valor positivo.")]
        public double Height { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "A largura deve ser um valor positivo.")]
        public double Width { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "A profundidade deve ser um valor positivo.")]
        public double Depth { get; set; }

        [Required(ErrorMessage = "A unidade de medida é obrigatória.")]
        [StringLength(10, ErrorMessage = "A unidade de medida não pode ter mais de 10 caracteres.")]
        public string Unit { get; set; } = "cm";
    }
    #endregion
}
