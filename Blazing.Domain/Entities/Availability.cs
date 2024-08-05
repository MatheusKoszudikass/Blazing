using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity Availability.
    /// <summary>
    /// Entity responsible for providing information on whether the product is available.
    /// </summary>
    public class Availability
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "A disponibilidade deve ser informada!.")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "A data estimada de entrega é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data estimada de entrega deve ser uma data valida.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EstimatedDeliveryDate { get; set; }
    }
    #endregion
}
