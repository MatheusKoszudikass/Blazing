using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity revision.
    /// <summary>
    /// Entity responsible for the product review made by the user.
    /// </summary>
    public sealed class Revision : BaseEntity
    {

        public string? User { get; set; }

        public string? Comment { get; set; }

        [Required(ErrorMessage = "A data da revisão é obrigatória.")]
        public DateTime Date { get; set; }
    }
    #endregion
}
