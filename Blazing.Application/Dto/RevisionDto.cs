using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blazing.Application.Dto
{
    #region DTO revision.
    /// <summary>
    /// DTO responsible for the product review made by the user.
    /// </summary>
    public sealed class RevisionDto : BaseEntityDto
    {
        public string? User { get; set; }

        public string? Comment { get; set; }

        [Required(ErrorMessage = "A data da revisão é obrigatória.")]
        public DateTime Date { get; set; } = DateTime.Now;
    } 
    #endregion
}
